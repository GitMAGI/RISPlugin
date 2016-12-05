using DataAccessLayer;
using BusinessLogicLayer;
using IBLL.DTO;
using System.Collections.Generic;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Start test ... ");

            RISDAL dal = new RISDAL();
            RISBLL bll = new RISBLL(dal);

            //string richID = "20160804111023719";
            //string episID = "490937";

            //RichiestaRISDTO rich = bll.GetRichiestaRISById(richID);
            //List<EsameDTO> esams = bll.GetEsamiByRich(richID);
            //List<EsameDTO> esams = bll.GetEsamiByEpis(episID);

            System.Console.WriteLine("Press a Key to Complete the test!");
            System.Console.ReadKey();
            System.Console.WriteLine("Test Complete!");
        }
    }
}
