using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Mappers;
using System.Diagnostics;
using GeneralPurposeLib;

namespace BusinessLogicLayer
{
    public partial class RISBLL
    {
        public List<IBLL.DTO.EsameDTO> GetEsamiByRich(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.EsameDTO> esams = null;

            try
            {
                log.Info(string.Format("Getting RichiestaRIS by ID:'{0}' ... ", richidid));
                IBLL.DTO.RichiestaRISDTO rich = GetRichiestaRISById(richidid);
                if (rich != null)
                {
                    if(rich.esami != null && rich.esami != string.Empty)
                    {
                        log.Info(string.Format("Fetching IDs esame ... "));
                        string esamids_ = rich.esami;
                        string[] esamids = esamids_.Split(',');

                        if (esamids.Length >= 0)
                        {
                            log.Info(string.Format("Got {0} ID esame incapsulated into RichiestaRIS with ID:'{1}'!", esamids.Length, richidid));
                            foreach (string esamid in esamids)
                            {
                                IBLL.DTO.EsameDTO tmp = GetEsameById(esamid);
                                if (tmp != null)
                                {
                                    log.Info(string.Format("Adding esame with ID:'{0}' to Collection ... ", tmp.esameidid));
                                    if (esams == null)
                                        esams = new List<IBLL.DTO.EsameDTO>();
                                    esams.Add(tmp);
                                }
                            }
                            log.Info(string.Format("Built a Collection with {0} esami!", esams.Count));
                        }
                    }
                    else
                    {
                        log.Info(string.Format("No IDs esame in this RichiestaRIS!"));
                    }                                   
                }                
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esams;
        }

        public List<IBLL.DTO.EsameDTO> GetEsamiByEpis(string episidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IBLL.DTO.EsameDTO> esams = null;

            try
            {
                log.Info(string.Format("Getting RichiestaRIS by Epis ID:'{0}' ... ", episidid));
                List<IBLL.DTO.RichiestaRISDTO> richs = GetRichiesteRISByEpis(episidid);
                if (richs != null)
                {
                    foreach(IBLL.DTO.RichiestaRISDTO rich in richs)
                    {
                        if (rich != null)
                        {
                            log.Info(string.Format("Getting Esami incapsulated into RichiestaRIS with ID:'{0}' ... ", rich.objectid));
                            List<IBLL.DTO.EsameDTO> esams_ = GetEsamiByRich(rich.objectid);
                            if (esams_ != null)
                            {
                                log.Info(string.Format("Adding Range with {0} items to Collection ... ", esams_.Count));
                                if (esams == null)
                                    esams = new List<IBLL.DTO.EsameDTO>();
                                esams.AddRange(esams_);
                            }
                        }                            
                    }
                    log.Info(string.Format("Built a Collection with {0} esami!", esams.Count));
                }
                log.Info(string.Format("{0} VO mapped to {1}", esams.Count, esams.First().GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esams;
        }
        
        public IBLL.DTO.EsameDTO GetEsameById(string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.EsameDTO esam = null;

            try
            {
                IDAL.VO.EsameVO dalRes = this.dal.GetEsameById(esamidid);
                esam = EsameMapper.EsamMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", esam.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esam;
        }
        public int UpdateEsameById(IBLL.DTO.EsameDTO data, string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                IDAL.VO.EsameVO data_ = EsameMapper.EsamMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetEsame(data_, esamidid);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public int AddEsame(IBLL.DTO.EsameDTO data)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                IDAL.VO.EsameVO data_ = EsameMapper.EsamMapper(data);
                log.Info(string.Format("1 {0} mapped to {1}", data.GetType().ToString(), data_.GetType().ToString()));
                result = dal.SetEsame(data_);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public int DeleteEsameById(string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            int result = 0;

            try
            {
                result = dal.DeleteEsame(esamidid);
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
    }
}
