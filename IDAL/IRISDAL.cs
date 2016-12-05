using System.Collections.Generic;

namespace IDAL
{
    public interface IRISDAL
    {
        VO.EventoVO GetEventoById(string evenidid);
        int SetEvento(VO.EventoVO data);
        VO.EventoVO NewEvento(VO.EventoVO data);
        int DeleteEventoById(string evenidid);

        List<VO.EsameVO> GetEsamiByRichiesta(string richidid);
        VO.EsameVO GetEsameById(string esamidid);
        List<VO.EsameVO> GetEsamiByIds(List<string> esamids);
        int SetEsame(VO.EsameVO data);
        VO.EsameVO NewEsame(VO.EsameVO data);
        List<VO.EsameVO> NewEsami(List<VO.EsameVO> data);
        int DeleteEsameById(string radioidid);
        int DeleteEsameByRichiesta(string richidid);
                
        VO.RichiestaRISVO GetRichiestaById(string presidid);
        List<VO.RichiestaRISVO> GetRichiesteByEven(string evenidid);
        int SetRichiesta(VO.RichiestaRISVO data);
        VO.RichiestaRISVO NewRichiesta(VO.RichiestaRISVO data);
        int DeleteRichiestaById(string presidid);

        string SendRISRequest(string richidid);
    }
}
