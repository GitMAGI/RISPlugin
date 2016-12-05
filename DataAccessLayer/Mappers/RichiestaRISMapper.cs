using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Mappers
{
    public class RichiestaRISMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<IDAL.VO.RichiestaRISVO> RichMapper(DataTable rows)
        {
            List<IDAL.VO.RichiestaRISVO> rich = null;
            if (rows != null)
            {
                rich = new List<IDAL.VO.RichiestaRISVO>();
                foreach (DataRow row in rows.Rows)
                {
                    rich.Add(RichMapper(row));
                }
            }
            return rich;
        }
        public static IDAL.VO.RichiestaRISVO RichMapper(DataRow row)
        {
            IDAL.VO.RichiestaRISVO rich = new IDAL.VO.RichiestaRISVO();

            rich.presidid = row["presidid"] != DBNull.Value ? (int)row["presidid"] : (int?)null;
            rich.preseven = row["preseven"] != DBNull.Value ? (int)row["preseven"] : (int?)null;
            rich.presques = row["presques"] != DBNull.Value ? (string)row["presques"] : null;
            rich.prescons = row["prescons"] != DBNull.Value ? (string)row["prescons"] : null;
            rich.presstat = row["presstat"] != DBNull.Value ? (short)row["presstat"] : (short?)null;
            rich.prestipo = row["prestipo"] != DBNull.Value ? (int)row["prestipo"] : (int?)null;
            rich.presurge = row["presurge"] != DBNull.Value ? (bool)row["presurge"] : (bool?)null;
            rich.prespren = row["prespren"] != DBNull.Value ? (DateTime)row["prespren"] : (DateTime?)null;
            rich.presrico = row["presrico"] != DBNull.Value ? (int)row["presrico"] : (int?)null;
            rich.presesec = row["presesec"] != DBNull.Value ? (DateTime)row["presesec"] : (DateTime?)null;
            rich.presflcc = row["presflcc"] != DBNull.Value ? (int)row["presflcc"] : (int?)null;
            rich.presconf = row["presconf"] != DBNull.Value ? (int)row["presconf"] : (int?)null;
            rich.presdmod = row["presdmod"] != DBNull.Value ? (string)row["presdmod"] : null;
            rich.presnote = row["presnote"] != DBNull.Value ? (string)row["presnote"] : null;
            rich.presdtri = row["presdtri"] != DBNull.Value ? (DateTime)row["presdtri"] : (DateTime?)null;
            rich.presdtco = row["presdtco"] != DBNull.Value ? (DateTime)row["presdtco"] : (DateTime?)null;
            rich.prespers = row["prespers"] != DBNull.Value ? (string)row["prespers"] : null;
            rich.preserog = row["preserog"] != DBNull.Value ? (short)row["preserog"] : (short?)null;
            rich.prespren2 = row["prespren2"] != DBNull.Value ? (DateTime)row["prespren2"] : (DateTime?)null;
            rich.presdimi = row["presdimi"] != DBNull.Value ? (int)row["presdimi"] : (int?)null;
            rich.presecocardio = row["presecocardio"] != DBNull.Value ? (int)row["presecocardio"] : (int?)null;
            rich.presvisicardio = row["presvisicardio"] != DBNull.Value ? (int)row["presvisicardio"] : (int?)null;
            rich.presappu = row["presappu"] != DBNull.Value ? (long)row["presappu"] : (long?)null;
            rich.presannu = row["presannu"] != DBNull.Value ? (int)row["presannu"] : (int?)null;
            rich.hl7_stato = row["hl7_stato"] != DBNull.Value ? (string)row["hl7_stato"] : null;
            rich.hl7_msg = row["hl7_msg"] != DBNull.Value ? (string)row["hl7_msg"] : null;
            rich.prespadre = row["prespadre"] != DBNull.Value ? (int)row["prespadre"] : (int?)null;
            rich.presconscardio = row["presconscardio"] != DBNull.Value ? (int)row["presconscardio"] : (int?)null;
            rich.prespagatipo = row["prespagatipo"] != DBNull.Value ? (int)row["prespagatipo"] : (int?)null;
            rich.prespagastat = row["prespagastat"] != DBNull.Value ? (int)row["prespagastat"] : (int?)null;
            rich.prespagadata = row["prespagadata"] != DBNull.Value ? (DateTime)row["prespagadata"] : (DateTime?)null;
            rich.prespagauser = row["prespagauser"] != DBNull.Value ? (int)row["prespagauser"] : (int?)null;
            rich.prescdc = row["prescdc"] != DBNull.Value ? (string)row["prescdc"] : null;

            return rich;
        }        
    }
}