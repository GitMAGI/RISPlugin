using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class EsameMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /*
        public static IDAL.VO.EsameVO EsamMapper(hlt_esameradio data)
        {
            IDAL.VO.EsameVO esam = null;

            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<hlt_esameradio, IDAL.VO.EsameVO>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<IDAL.VO.EsameVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }
        public static List<IDAL.VO.EsameVO> EsamMapper(List<hlt_esameradio> data)
        {
            List<IDAL.VO.EsameVO> esams = null;

            if (data != null && data.Count > 0)
            {
                esams = new List<IDAL.VO.EsameVO>();

                foreach (hlt_esameradio datum in data)
                {
                    IDAL.VO.EsameVO tmp = null;

                    tmp = EsamMapper(datum);

                    if (tmp != null)
                        esams.Add(tmp);
                }
            }

            return esams;
        }
        public static hlt_esameradio EsamMapper(IDAL.VO.EsameVO data)
        {
            hlt_esameradio esam = null;

            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.EsameVO, hlt_esameradio>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<hlt_esameradio>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }
        */

        public static IDAL.VO.EsameVO EsamMapper(DataRow row)
        {
            IDAL.VO.EsameVO esam = new IDAL.VO.EsameVO();

            esam.esameidid = row["esameidid"] != DBNull.Value ? (long)row["esameidid"] : (long?)null;
            esam.esamedesc = row["esamedesc"] != DBNull.Value ? (string)row["esamedesc"] : null;
            esam.esametipo = row["esametipo"] != DBNull.Value ? (int)row["esametipo"] : (int?)null;
            esam.esamestato = row["esamestato"] != DBNull.Value ? (string)row["esamestato"] : null;
            esam.esamedataprenotazione = row["esamedataprenotazione"] != DBNull.Value ? (DateTime)row["esamedataprenotazione"] : (DateTime?)null;
            esam.esamedataesecuzione = row["esamedataesecuzione"] != DBNull.Value ? (DateTime)row["esamedataesecuzione"] : (DateTime?)null;
            esam.esamereferto = row["esamereferto"] != DBNull.Value ? (string)row["esamereferto"] : null;
            esam.esame_ext_key = row["esame_ext_key"] != DBNull.Value ? (string)row["esame_ext_key"] : null;
            esam.esamerichid = row["esamerichid"] != DBNull.Value ? (string)row["esamerichid"] : null;

            return esam;
        }
        public static List<IDAL.VO.EsameVO> EsamMapper(DataTable rows)
        {
            List<IDAL.VO.EsameVO> data = new List<IDAL.VO.EsameVO>();

            if (rows != null)
            {
                if(rows.Rows.Count > 0)
                {
                    foreach(DataRow row in rows.Rows)
                    {
                        IDAL.VO.EsameVO tmp = EsamMapper(row);
                        data.Add(tmp);
                    }
                }
            }

            return data;
        }
    }
}
