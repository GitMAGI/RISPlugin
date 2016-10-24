using System.Collections.Generic;

namespace IDAL
{
    public interface IRISDAL
    {
        VO.PazienteVO GetPazienteById(string pazidid);
        VO.EpisodioVO GetEpisodioById(string episidid);

        VO.RichiestaRISVO GetRichiestaById(string richidid);
        List<VO.RichiestaRISVO> GetRichiesteByEpis(string episidid);
        int SetRichiesta(VO.RichiestaRISVO data, string richidid = null);

        VO.EsameVO GetEsameById(string esamidid);
        int SetEsame(VO.EsameVO data, string esamidid = null);
        int DeleteEsame(string esamidid);
    }
}
