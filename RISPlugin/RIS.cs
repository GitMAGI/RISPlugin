using IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RISPlugin
{
    public class RIS : IRISPlugin.IRIS
    {
        private static readonly log4net.ILog log =
               log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataAccessLayer.RISDAL dal;
        private BusinessLogicLayer.RISBLL bll;

        public object LibString { get; private set; }

        public RIS()
        {
            dal = new DataAccessLayer.RISDAL();
            bll = new BusinessLogicLayer.RISBLL(dal);
        }

        public string ScheduleNewRequest(RichiestaRISDTO rich, List<EsameDTO> radios, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string hl7_stato = IBLL.HL7StatesRichiestaRIS.Idle;
            string res = null;

            RichiestaRISDTO presInserted = null;
            List<EsameDTO> radiosInserted = null;

            if (errorString == null)
                errorString = "";

            try
            {
                if (rich == null || radios == null || (radios != null && radios.Count == 0))
                    throw new Exception("Error! Request a null or void insertion of PRES and/or RADIO");

                // Validation Pres!!!!
                if (!bll.ValidatePres(rich, ref errorString))
                {
                    string msg = "Validation Esam Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                // Check if Even Exists
                string evenid = rich.preseven.ToString();
                EventoDTO even = bll.GetEventoById(evenid);
                if (even == null)
                {
                    string msg = "Error! Even with evenidid: " + evenid + " doesn't exist! the Scheduling of the request will be aborted!";
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }
                even = null;

                rich.hl7_stato = hl7_stato;
                log.Info(string.Format("PRES Insertion ..."));
                presInserted = bll.AddRichiestaRIS(rich);
                if (presInserted == null)
                    throw new Exception("Error during PRES writing into the DB");
                log.Info(string.Format("PRES Inserted. Got {0} PRESIDID!", presInserted.presidid));

                res = presInserted.presidid.ToString();

                radios.ForEach(p => { p.radiopres = int.Parse(res); p.hl7_stato = hl7_stato; });

                // Validation Radios!!!!
                if (!bll.ValidateRadios(radios, ref errorString))
                {
                    string msg = "Validation Anals Failure! Check the error string for figuring out the issue!";
                    log.Info(msg + "\r\n" + errorString);
                    log.Error(msg + "\r\n" + errorString);
                    throw new Exception(msg);
                }

                log.Info(string.Format("Insertion of {0} RADIO requested. Processing ...", radios.Count));
                radiosInserted = bll.AddEsami(radios);
                if ((radiosInserted == null) || (radiosInserted != null && radiosInserted.Count != radios.Count))
                    throw new Exception("Error during RADIOs writing into the DB");
                log.Info(string.Format("Inserted {0} RADIO successfully!", radiosInserted.Count));
                log.Info(string.Format("Inserted {0} records successfully!", radiosInserted.Count + 1));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);

                if (errorString == "")
                    errorString = msg + "\r\n" + ex.Message;
                else
                    errorString += "\r\n" + msg + "\r\n" + ex.Message;

                int esamRB = 0;
                int analsRB = 0;

                log.Info(string.Format("Rolling Back of the Insertions due an error occured ..."));
                // Rolling Back
                if (res != null)
                {
                    esamRB = bll.DeleteRichiestaRISById(res);
                    log.Info(string.Format("Rolled Back {0} PRES record. PRESIDID was {1}!", esamRB, res));
                    analsRB = bll.DeleteEsamiByRichiesta(res);
                    log.Info(string.Format("Rolled Back {0} RADIO records. RADIOPRES was {1}!", analsRB, res));
                }
                log.Info(string.Format("Rolled Back {0} records of {1} requested!", esamRB + analsRB, radios.Count + 1));
                res = null;
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));    

            if (errorString == "")
                errorString = null;

            return res;
        }
        public MirthResponseDTO SubmitNewRequest(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            if (errorString == null)
                errorString = "";

            try
            {
                // 0. Check if richid is a numeric Value
                int richid_int = 0;
                if (!int.TryParse(richid, out richid_int))
                {
                    string msg = string.Format("ID of the riquest is not an integer string. {0} is not a valid ID for this context!", richid);
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 1. Check if ESAM and ANAL exist
                RichiestaRISDTO chkRich = bll.GetRichiestaRISById(richid);
                List<EsameDTO> chkRadios = bll.GetEsamiByRichiesta(richid);
                if (chkRich == null || chkRadios == null || (chkRadios != null && chkRadios.Count == 0))
                {
                    string msg = "Error! No Esam or Anal records found referring to EsamID " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 2. Settare Stato a "SEDNING"
                int res = bll.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Sending, "");

                // 3. Invio a Mirth
                string hl7orl = bll.SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 3.e1 Cambiare stato in errato
                    int err = bll.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Errored, msg);
                    // 3.e2 Restituire null
                    return null;
                }
                // 3.1 Settare a SETN
                int snt = bll.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Sent, "");

                // 4. Estrarre i dati dalla risposta di Mirth                
                log.Info("Mirth Data Response Extraction ...");
                data = bll.ORLParser(hl7orl);
                if (data == null)
                {
                    string emsg = "Mirth Data Response Extraction failed!";
                    if (errorString == "")
                        errorString = emsg;
                    else
                        errorString += "\n\r" + emsg;
                    log.Info(emsg);
                    log.Error(emsg);

                }
                else
                    log.Info("Mirth Data Response Successfully extracted!");

                // 5. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaRIS.Sent;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaRIS.Errored;

                RichiestaRISDTO RichUpdt = bll.ChangeHL7StatusAndMessageRichiestaRIS(richid, status, data.ACKDesc);

                List<ORCStatus> orcs = data.ORCStatus;
                if (orcs != null)
                {
                    foreach (ORCStatus orc in orcs)
                    {
                        string desc = orc.Description;
                        string stat = orc.Status;
                        string analid = orc.RadioID;
                        List<EsameDTO> AnalUpdts = bll.ChangeHL7StatusAndMessageEsami(new List<string>() { analid }, stat, desc);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            // 6. Restituire il DTO
            return data;
        }
        
        public List<EsameDTO> Check4Radios(string richid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<EsameDTO> radios = null;

            radios = bll.GetEsamiByRichiesta(richid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return radios;
        }
        public List<RichiestaRISDTO> Check4Richs(string evenid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<RichiestaRISDTO> exams = null;

            exams = bll.GetRichiesteRISByEven(evenid);

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return exams;
        }
        
        public MirthResponseDTO CancelRequest(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = null;

            try
            {
                // 0. Check if richid is a numeric Value
                int richid_int = 0;
                if (!int.TryParse(richid, out richid_int))
                {
                    string msg = string.Format("ID of the riquest is not an integer string. {0} is not a valid ID for this context!", richid);
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 1. Check if Canceling is allowed
                if (CheckIfCancelingIsAllowed(richid, ref errorString))
                {
                    string msg = string.Format("Canceling of the request with id {0} is denied! errorString: {1}", richid, errorString);
                    log.Info(msg);
                    log.Error(msg);
                    throw new Exception(msg);
                }

                // 2. Check if PRES and RADIO exist
                RichiestaRISDTO chkRich = bll.GetRichiestaRISById(richid);
                List<EsameDTO> chkRadios = bll.GetEsamiByRichiesta(richid);
                if (chkRich == null || chkRadios == null || (chkRadios != null && chkRadios.Count == 0))
                {
                    string msg = "Error! No Pres or Radio records found referring to RichID " + richid + "! A request must be Scheduled first!";
                    errorString = msg;
                    log.Info(msg);
                    log.Error(msg);
                    return null;
                }

                // 3. Settare Stato a "DELETNG"
                int res = bll.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Deleting);

                // 4. Invio a Mirth
                string hl7orl = bll.SendMirthRequest(richid);
                if (hl7orl == null)
                {
                    string msg = "Mirth Returned an Error!";
                    errorString = msg;
                    // 4.e1 Cambiare stato in errato
                    int err = bll.ChangeHL7StatusAndMessageAll(richid, IBLL.HL7StatesRichiestaRIS.Errored, msg);
                    // 4.e2 Restituire null
                    return null;
                }

                // 5. Estrarre i dati dalla risposta di Mirth
                data = bll.ORLParser(hl7orl);

                // 6. Settare Stato a seconda della risposta
                string status = IBLL.HL7StatesRichiestaRIS.Deleted;
                if (data.ACKCode != "AA")
                    status = IBLL.HL7StatesRichiestaRIS.Errored;
                RichiestaRISDTO RichUpdt = bll.ChangeHL7StatusAndMessageRichiestaRIS(richid, status, data.ACKDesc);

                List<ORCStatus> orcs = data.ORCStatus;
                if (orcs != null)
                    foreach (ORCStatus orc in orcs)
                    {
                        string desc = orc.Description;
                        string stat = orc.Status;
                        string analid = orc.RadioID;
                        List<EsameDTO> RadioUpdts = bll.ChangeHL7StatusAndMessageEsami(new List<string>() { analid }, stat, desc);
                    }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return data;
        }
        public bool CheckIfCancelingIsAllowed(string richid, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool res = true;

            if (errorString == null)
                errorString = "";

            List<EsameDTO> radios = bll.GetEsamiByRichiesta(richid);
            foreach (EsameDTO radio in radios)
            {
                if(radio.radiorefe!=null)
                {
                    string report = string.Format("Esame {0} già eseguito! Impossibile Cancellare!", radio.radioidid.Value.ToString());
                    res = false;
                    if (errorString != "")
                        errorString += "\r\n" + report;
                    else
                        errorString += report;
                }
            }


            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
    }
}
