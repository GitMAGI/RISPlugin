using GeneralPurposeLib;
using IBLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class RISBLL
    {
        public MirthResponseDTO ORLParser(string raw)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            MirthResponseDTO data = new MirthResponseDTO();

            log.Info(string.Format("HL7 Message To Process:\n{0}", raw));

            log.Info(string.Format("HL7 Message Processing ... "));

            try
            {

                log.Info(string.Format("MSA Recovering ..."));
                // 1. Get MSA Segment
                string msa = LibString.GetAllValuesSegments(raw, "MSA")[0];
                string[] msaobj = msa.Split('|');
                data.ACKCode = msaobj[1];
                data.MsgID = msaobj[2];
                data.ACKDesc = msaobj.Length > 3 ? msaobj[2] : null;
                switch (data.ACKCode)
                {
                    case "AA":
                        data.Errored = false;
                        data.Accepted = true;
                        data.Refused = false;
                        break;
                    case "AE":
                        data.Errored = true;
                        data.Accepted = false;
                        data.Refused = false;
                        break;
                    case "AR":
                        data.Errored = false;
                        data.Accepted = false;
                        data.Refused = true;
                        break;
                }
                log.Info(string.Format("MSA Recovered"));

                // 2. Get ERR Segment            
                log.Info(string.Format("ERR Recovering ..."));
                List<string> errs = LibString.GetAllValuesSegments(raw, "ERR");
                if (errs != null)
                    data.ERRMsg = errs[0];
                log.Info(string.Format("ERR Recovered"));
                // 3. Get ORC Segment
                log.Info(string.Format("ORC Recovering ..."));
                List<string> orcs = LibString.GetAllValuesSegments(raw, "ORC");
                if (orcs != null)
                {
                    foreach (string orc in orcs)
                    {
                        try
                        {
                            string[] ocrobj = orc.Split('|');
                            ORCStatus ORC = new ORCStatus();
                            string[] esIdanId = ocrobj[2].Split('-');
                            ORC.PresID = esIdanId[0];
                            ORC.RadioID = esIdanId[1];
                            ORC.Status = ocrobj[1];
                            string desc = null;
                            switch (ORC.Status)
                            {
                                case "OK":
                                    desc = "Inserimento/Cancellazione eseguito con successo";
                                    break;
                                case "RQ":
                                    desc = "Modifica Eseguita con successo";
                                    break;
                                case "UA":
                                    desc = "Impossibile Inserire";
                                    break;
                                case "UC":
                                    desc = "Impossibile Cancellare";
                                    break;
                                case "UM":
                                    desc = "Impossibile Modificare";
                                    break;
                            }
                            ORC.Description = desc;
                            if (data.ORCStatus == null)
                                data.ORCStatus = new List<ORCStatus>();
                            data.ORCStatus.Add(ORC);
                        }
                        catch (Exception)
                        {
                            string msg = "Exception During ORC info processing! HL7 Segment errored: " + orc;
                            throw new Exception(msg);
                        }
                    }
                }
                log.Info(string.Format("ORC Recovered", data.ORCStatus.Count));                

                log.Info(string.Format("HL7 Message processing Complete! A DTO object has been built!"));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }

        public string SendMirthRequest(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            // 0. Check if pres exists and radios do
            RichiestaRISDTO rich = GetRichiestaRISById(richidid);
            List<EsameDTO> esams = GetEsamiByRichiesta(richidid);

            if ((rich == null || esams == null) || (esams != null && esams.Count == 0))
            {
                string msg = string.Format("An Error occured! No PRES or RADIO related to id {0} found into the DB. Operation Aborted!", richidid);
                log.Info(msg);
                log.Error(msg);
                return null;
            }

            string data = null;

            try
            {
                // 1. Call DAL.SendMirthREquest()
                data = this.dal.SendRISRequest(richidid);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }

            return data;
        }
        public int ChangeHL7StatusAndMessageAll(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int res = 0;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));
            log.Info(string.Format("Updating PRES ..."));

            RichiestaRISDTO got = GetRichiestaRISById(richidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            RichiestaRISDTO updt = UpdateRichiestaRIS(got);

            int esamres = 0;
            if (updt != null)
                esamres++;
            else
                log.Info(string.Format("An Error occurred. Record not updated! ESAMIDID: {0}", got.presidid));
            res = esamres;

            log.Info(string.Format("Updated {0}/{1} record!", esamres, 1));

            log.Info(string.Format("Updating ANAL ..."));
            List<EsameDTO> gots = GetEsamiByRichiesta(richidid);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (EsameDTO got_ in gots)
            {
                EsameDTO updt_ = UpdateEsame(got_);
                if (updt_ != null)
                    analsres++;
                else
                    log.Info(string.Format("An Error occurred. Record not updated! RADIOIDID: {0}", got_.radioidid));
            }
            res += analsres;
            log.Info(string.Format("Updated {0}/{1} record!", analsres, gots.Count));

            log.Info(string.Format("Updated {0} record overall!", res));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return res;
        }
        public List<EsameDTO> ChangeHL7StatusAndMessageEsami(List<string> esamids, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            List<EsameDTO> updateds = null;

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));
            
            log.Info(string.Format("Updating RADIO ..."));
            List<EsameDTO> gots = GetEsamiByIds(esamids);
            gots.ForEach(p => { p.hl7_stato = hl7_stato; p.hl7_msg = hl7_msg != null ? hl7_msg : p.hl7_msg; });
            int analsres = 0;
            foreach (EsameDTO got_ in gots)
            {
                EsameDTO updt_ = UpdateEsame(got_);
                if (updt_ != null)
                {
                    if (updateds == null)
                        updateds = new List<EsameDTO>();
                    updateds.Add(updt_);
                    analsres++;
                }
                else
                    log.Info(string.Format("An Error occurred. Record not updated!RADIOIDID: {0}", got_.radioidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", analsres, gots.Count));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updateds;
        }
        public RichiestaRISDTO ChangeHL7StatusAndMessageRichiestaRIS(string richidid, string hl7_stato, string hl7_msg = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            RichiestaRISDTO updated = new RichiestaRISDTO();

            log.Info(string.Format("Starting ..."));

            string msg_ = "Status updating with 'hl7_stato'-> " + hl7_stato;
            if (hl7_msg != null)
                msg_ += " and 'hl7_msg'-> " + hl7_msg;
            log.Info(string.Format(msg_));

            log.Info(string.Format("Updating PRES ..."));

            RichiestaRISDTO got = GetRichiestaRISById(richidid);
            got.hl7_stato = hl7_stato;
            got.hl7_msg = hl7_msg != null ? hl7_msg : got.hl7_msg;
            updated = UpdateRichiestaRIS(got);

            int res = 0;
            if (updated != null)
            {
                res++;
            }
            else
            {
                log.Info(string.Format("An Error occurred. Record not updated! PRESIDID: {0}", got.presidid));
            }
            log.Info(string.Format("Updated {0}/{1} record!", res, 1));

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return updated;
        }

        public bool ValidatePres(RichiestaRISDTO pres, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";
            
            if (pres.preseven == null)
            {
                string msg = "PRESEVEN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "PRES error: " + msg;
                else
                    errorString += "PRES error: " + msg;
            }
            if (pres.prestipo == null)
            {
                string msg = "PRESTIPO is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "PRES error: " + msg;
                else
                    errorString += "PRES error: " + msg;
            }
            if (pres.prespren == null)
            {
                string msg = "PRESPREN is Null!";
                validate = false;
                if (errorString != "")
                    errorString += "\r\n" + "PRES error: " + msg;
                else
                    errorString += "PRES error: " + msg;
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
        public bool ValidateRadios(List<EsameDTO> radios, ref string errorString)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            bool validate = true;

            if (errorString == null)
                errorString = "";

            int count = 0;
            foreach (EsameDTO radio in radios)
            {
                count++;
                
                if (radio.radiopres == null)
                {
                    string msg = "RADIOPRES is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
                if (radio.radiotipo == null)
                {
                    string msg = "RADIOTIPO is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
                if (radio.radiodesc == null)
                {
                    string msg = "RADIODESC is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
                if (radio.radiodass == null)
                {
                    string msg = "RADIODASS is Null!";
                    validate = false;
                    if (errorString != "")
                        errorString += "\r\n" + "RADIO (" + count + ") error: " + msg;
                    else
                        errorString += "RADIO (" + count + ") error: " + msg;
                }
            }

            if (errorString == "")
                errorString = null;

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return validate;
        }
    }
}
