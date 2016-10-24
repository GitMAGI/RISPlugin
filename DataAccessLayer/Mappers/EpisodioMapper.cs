using System;
using System.Data;
using AutoMapper;

namespace DataAccessLayer.Mappers
{
    public class EpisodioMapper
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        

        public static IDAL.VO.EpisodioVO EpisMapper(DataRow row)
        {
            IDAL.VO.EpisodioVO epis = new IDAL.VO.EpisodioVO();

            epis.codice = row["codice"] != DBNull.Value ? (int)row["codice"] : 0;
            epis.cartella = row["cartella"] != DBNull.Value ? (string)row["cartella"] : null;
            epis.tipo = row["tipo"] != DBNull.Value ? (string)row["tipo"] : null;
            epis.data = row["data"] != DBNull.Value ? (string)row["data"].ToString() : null;
            epis.ora = row["ora"] != DBNull.Value ? (string)row["ora"].ToString() : null;
            epis.dimissione = row["dimissione"] != DBNull.Value ? (string)row["dimissione"].ToString() : null;
            epis.ora_dimiss = row["ora_dimiss"] != DBNull.Value ? (string)row["ora_dimiss"].ToString() : null;
            epis.camera = row["camera"] != DBNull.Value ? (string)row["camera"] : null;
            epis.reparto = row["reparto"] != DBNull.Value ? (string)row["reparto"] : null;
            epis.convenzione1 = row["convenzione1"] != DBNull.Value ? (string)row["convenzione1"] : null;
            epis.convenzione2 = row["convenzione2"] != DBNull.Value ? (string)row["convenzione2"] : null;
            epis.impegnativa = row["impegnativa"] != DBNull.Value ? (string)row["impegnativa"] : null;
            epis.data_impegn = row["data_impegn"] != DBNull.Value ? (string)row["data_impegn"].ToString() : null;
            epis.giorni = row["giorni"] != DBNull.Value ? (short)row["giorni"] : (short)0;
            epis.usl = row["usl"] != DBNull.Value ? (string)row["usl"] : null;
            epis.regione = row["regione"] != DBNull.Value ? (string)row["regione"] : null;
            epis.tessera = row["tessera"] != DBNull.Value ? (string)row["tessera"] : null;
            epis.diagnosi = row["diagnosi"] != DBNull.Value ? (string)row["diagnosi"] : null;
            epis.provenienza = row["provenienza"] != DBNull.Value ? (string)row["provenienza"] : null;
            epis.regime = row["regime"] != DBNull.Value ? (string)row["regime"] : null;
            epis.inviante = row["inviante"] != DBNull.Value ? (int)row["inviante"] : 0;
            epis.accettante = row["accettante"] != DBNull.Value ? (int)row["accettante"] : 0;
            epis.consegna = row["consegna"] != DBNull.Value ? (string)row["consegna"].ToString() : null;
            epis.totale = row["totale"] != DBNull.Value ? (double)row["totale"] : 0;
            epis.dovuto = row["dovuto"] != DBNull.Value ? (double)row["dovuto"] : 0;
            epis.acconto = row["acconto"] != DBNull.Value ? (double)row["acconto"] : 0;
            epis.stato = row["stato"] != DBNull.Value ? (string)row["stato"] : null;
            epis.tipo_stato = row["tipo_stato"] != DBNull.Value ? (string)row["tipo_stato"] : null;
            epis.seriale = row["seriale"] != DBNull.Value ? (int)row["seriale"] : 0;
            epis.privacy = row["privacy"] != DBNull.Value ? (short)row["privacy"] : (short)0;
            epis.anonimato = row["anonimato"] != DBNull.Value ? (short)row["anonimato"] : (short)0;
            epis.data_open = row["data_open"] != DBNull.Value ? (string)row["data_open"].ToString() : null;
            epis.pazext = row["pazext"] != DBNull.Value ? (string)row["pazext"] : null;
            epis.argos_nos = row["argos_nos"] != DBNull.Value ? (string)row["argos_nos"] : null;
            epis.bloccato = row["bloccato"] != DBNull.Value ? (string)row["bloccato"] : null;
            epis.id_invio = row["id_invio"] != DBNull.Value ? (short)row["id_invio"] : (short)0;
            epis.dt_invio = row["dt_invio"] != DBNull.Value ? (string)row["dt_invio"] : null;
            epis.dt_pren = row["dt_pren"] != DBNull.Value ? (string)row["dt_pren"] : null;
            epis.dt_rice = row["dt_rice"] != DBNull.Value ? (string)row["dt_rice"] : null;
            epis.circoscrizione = row["circoscrizione"] != DBNull.Value ? (string)row["circoscrizione"] : null;
            epis.pnome = row["pnome"] != DBNull.Value ? (string)row["pnome"] : null;
            epis.pcognome = row["pcognome"] != DBNull.Value ? (string)row["pcognome"] : null;
            epis.pluogo_nascita = row["pluogo_nascita"] != DBNull.Value ? (string)row["pluogo_nascita"] : null;
            epis.ppaese = row["ppaese"] != DBNull.Value ? (string)row["ppaese"] : null;
            epis.pindirizzo = row["pindirizzo"] != DBNull.Value ? (string)row["pindirizzo"] : null;
            epis.pcomune = row["pcomune"] != DBNull.Value ? (string)row["pcomune"] : null;
            epis.pcap = row["pcap"] != DBNull.Value ? (string)row["pcap"] : null;
            epis.pprefisso = row["pprefisso"] != DBNull.Value ? (string)row["pprefisso"] : null;
            epis.ptelefono = row["ptelefono"] != DBNull.Value ? (string)row["ptelefono"] : null;
            epis.pcodice_fiscale = row["pcodice_fiscale"] != DBNull.Value ? (string)row["pcodice_fiscale"] : null;
            epis.pstato_civile = row["pstato_civile"] != DBNull.Value ? (string)row["pstato_civile"] : null;
            epis.pprofessione = row["pprofessione"] != DBNull.Value ? (string)row["pprofessione"] : null;
            epis.pdocumento = row["pdocumento"] != DBNull.Value ? (string)row["pdocumento"] : null;
            epis.pluogo_documento = row["pluogo_documento"] != DBNull.Value ? (string)row["pluogo_documento"] : null;
            epis.pdata_documento = row["pdata_documento"] != DBNull.Value ? (string)row["pdata_documento"] : null;
            epis.pdomicilio = row["pdomicilio"] != DBNull.Value ? (string)row["pdomicilio"] : null;
            epis.pcomune_domicilio = row["pcomune_domicilio"] != DBNull.Value ? (string)row["pcomune_domicilio"] : null;
            epis.pcurante = row["pcurante"] != DBNull.Value ? (int)row["pcurante"] : 0;
            epis.ImportoServizioDefault = row["ImportoServizioDefault"] != DBNull.Value ? (double)row["ImportoServizioDefault"] : 0;
            epis.domicilio_cap = row["domicilio_cap"] != DBNull.Value ? (string)row["domicilio_cap"] : null;
            epis.domicilio_comune = row["domicilio_comune"] != DBNull.Value ? (string)row["domicilio_comune"] : null;
            epis.domicilio_indirizzo = row["domicilio_indirizzo"] != DBNull.Value ? (string)row["domicilio_indirizzo"] : null;
            epis.domicilio_distretto = row["domicilio_distretto"] != DBNull.Value ? (string)row["domicilio_distretto"] : null;
            epis.cl_coge = row["cl_coge"] != DBNull.Value ? (string)row["cl_coge"] : null;
            epis.gestore = row["gestore"] != DBNull.Value ? (int)row["gestore"] : 0;
            epis.dovuto_privato = row["dovuto_privato"] != DBNull.Value ? (double)row["dovuto_privato"] : 0;
            epis.dovuto_assicurato = row["dovuto_assicurato"] != DBNull.Value ? (double)row["dovuto_assicurato"] : 0;

            return epis;
        }
        
    }
}
