using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer;

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
            IDAL.VO.RichiestaRISVO esam = new IDAL.VO.RichiestaRISVO();

            esam.data = row["data"] != DBNull.Value ? (string)row["data"].ToString() : null;
            esam.data_creazione = row["data_creazione"] != DBNull.Value ? (string)row["data_creazione"].ToString() : null;
            esam.data_modifica = row["data_modifica"] != DBNull.Value ? (string)row["data_modifica"].ToString() : null;
            esam.dimprotetta = row["dimprotetta"] != DBNull.Value ? (bool)row["dimprotetta"] : false;
            esam.esami = row["esami"] != DBNull.Value ? (string)row["esami"] : null;
            esam.idepisodio = row["idepisodio"] != DBNull.Value ? (string)row["idepisodio"] : null;
            esam.locker = row["locker"] != DBNull.Value ? (string)row["locker"] : null;
            esam.motivo = row["motivo"] != DBNull.Value ? (string)row["motivo"] : null;
            esam.nomeesami = row["nomeesami"] != DBNull.Value ? (string)row["nomeesami"] : null;
            esam.nomeutente_creazione = row["nomeutente_creazione"] != DBNull.Value ? (string)row["nomeutente_creazione"] : null;
            esam.nomeutente_modifica = row["nomeutente_modifica"] != DBNull.Value ? (string)row["nomeutente_modifica"] : null;
            esam.objectid = row["objectid"] != DBNull.Value ? (string)row["objectid"] : null;
            esam.ora = row["ora"] != DBNull.Value ? (string)row["ora"] : null;
            esam.pdfcreato = row["pdfcreato"] != DBNull.Value ? (string)row["pdfcreato"] : null;
            esam.quesitoclinico = row["quesitoclinico"] != DBNull.Value ? (string)row["quesitoclinico"] : null;
            esam.seriale = row["seriale"] != DBNull.Value ? (long)row["seriale"] : 0;
            esam.statopaziente = row["statopaziente"] != DBNull.Value ? (string)row["statopaziente"] : null;
            esam.urgente = row["urgente"] != DBNull.Value ? (bool)row["urgente"] : false;
            esam.versione = row["versione"] != DBNull.Value ? (string)row["versione"] : null;

            return esam; ;
        }
        
    }
}
