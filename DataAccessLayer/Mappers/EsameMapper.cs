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

            esam.radioidid = row["radioidid"] != DBNull.Value ? (int)row["radioidid"] : (int?)null;
            esam.radiopres = row["radiopres"] != DBNull.Value ? (int)row["radiopres"] : (int?)null;
            esam.radiodesc = row["radiodesc"] != DBNull.Value ? (string)row["radiodesc"] : null;
            esam.radiotipo = row["radiotipo"] != DBNull.Value ? (int)row["radiotipo"] : (int?)null;
            esam.radiodass = row["radiodass"] != DBNull.Value ? (DateTime)row["radiodass"] : (DateTime?)null;
            esam.es_dett_key = row["es_dett_key"] != DBNull.Value ? (string)row["es_dett_key"] : null;
            esam.es_data = row["es_data"] != DBNull.Value ? (DateTime)row["es_data"] : (DateTime?)null;
            esam.es_stato = row["es_stato"] != DBNull.Value ? (string)row["es_stato"] : null;
            esam.es_ref = row["es_ref"] != DBNull.Value ? (string)row["es_ref"] : null;
            esam.data_verifica = row["data_verifica"] != DBNull.Value ? (DateTime)row["data_verifica"] : (DateTime?)null;
            esam.esito_verifica = row["esito_verifica"] != DBNull.Value ? (string)row["esito_verifica"] : null;
            esam.es_data_referto = row["es_data_referto"] != DBNull.Value ? (DateTime)row["es_data_referto"] : (DateTime?)null;
            esam.es_data_validazione_referto = row["es_data_validazione_referto"] != DBNull.Value ? (DateTime)row["es_data_validazione_referto"] : (DateTime?)null;
            esam.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            esam.radioass2 = row["radioass2"] != DBNull.Value ? (string)row["radioass2"] : null;
            esam.radioass3 = row["radioass3"] != DBNull.Value ? (string)row["radioass3"] : null;
            esam.radioass1 = row["radioass1"] != DBNull.Value ? (string)row["radioass1"] : null;
            esam.radiolink = row["radiolink"] != DBNull.Value ? (string)row["radiolink"] : null;
            esam.radioass4 = row["radioass4"] != DBNull.Value ? (string)row["radioass4"] : null;
            esam.radiolink2 = row["radiolink2"] != DBNull.Value ? (string)row["radiolink2"] : null;
            esam.radiorefe = row["radiorefe"] != DBNull.Value ? (string)row["radiorefe"] : null;
            esam.radiorefestat = row["radiorefestat"] != DBNull.Value ? (string)row["radiorefestat"] : null;

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
