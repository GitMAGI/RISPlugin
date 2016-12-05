using System.Configuration;

namespace DataAccessLayer
{
    public partial class RISDAL : IDAL.IRISDAL
    {
        public static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string HLTDesktopConnectionString = ConfigurationManager.ConnectionStrings["HltDesktop"].ConnectionString;
        public string GRConnectionString = ConfigurationManager.ConnectionStrings["GR"].ConnectionString;
        public string CCConnectionString = ConfigurationManager.ConnectionStrings["CC"].ConnectionString;

        public string EsameTabName = ConfigurationManager.AppSettings["tbn_radio"];
        public string RichiestaRISTabName = ConfigurationManager.AppSettings["tbn_pres"];
        public string EventoTabName = ConfigurationManager.AppSettings["tbn_even"];
    }
}
