using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class EsameMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
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
