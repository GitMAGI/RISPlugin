using System.Collections.Generic;

namespace IBLL
{
    public interface IRISBLL
    {
        DTO.PazienteDTO GetPazienteById(string id);

        DTO.EpisodioDTO GetEpisodioById(string id);

        List<DTO.RichiestaRISDTO> GetRichiesteRISByEpis(string episid);
        DTO.RichiestaRISDTO GetRichiestaRISById(string richidid);

        List<DTO.EsameDTO> GetEsamiByRich(string richidid);
        List<DTO.EsameDTO> GetEsamiByEpis(string episidid);
        DTO.EsameDTO GetEsameById(string esamidid);
        int UpdateEsameById(DTO.EsameDTO data, string esamidid);
        int AddEsame(DTO.EsameDTO data);
        int DeleteEsameById(string esamidid);
    }
}