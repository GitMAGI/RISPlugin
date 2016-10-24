using BusinessLogicLayer.Mappers;
using GeneralPurposeLib;
using System;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public partial class RISBLL
    {
        public IBLL.DTO.EpisodioDTO GetEpisodioById(string id)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IBLL.DTO.EpisodioDTO epis = null;

            try
            {
                IDAL.VO.EpisodioVO dalRes = this.dal.GetEpisodioById(id);
                epis = EpisodioMapper.EpisMapper(dalRes);
                log.Info(string.Format("1 VO mapped to {0}", epis.GetType().ToString()));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            
            return epis;
        }

        public int SetEpisodioById(IBLL.DTO.EpisodioDTO data, string episidid)
        {
            int result = 0;

            return result;
        }
    }
}
