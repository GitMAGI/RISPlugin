using System.Collections.Generic;

namespace IBLL
{
    public interface IRISBLL
    {
        List<DTO.EsameDTO> GetEsamiByRichiesta(string richidid);
        DTO.EsameDTO GetEsameById(string esamidid);
        List<DTO.EsameDTO> GetEsamiByIds(List<string> esamidids);
        DTO.EsameDTO UpdateEsame(DTO.EsameDTO data);
        DTO.EsameDTO AddEsame(DTO.EsameDTO data);
        List<DTO.EsameDTO> AddEsami(List<DTO.EsameDTO> data);
        int DeleteEsameById(string esamidid);
        int DeleteEsamiByRichiesta(string richidid);

        List<DTO.RichiestaRISDTO> GetRichiesteRISByEven(string evenid);
        DTO.RichiestaRISDTO GetRichiestaRISById(string richidid);
        DTO.RichiestaRISDTO AddRichiestaRIS(DTO.RichiestaRISDTO data);
        DTO.RichiestaRISDTO UpdateRichiestaRIS(DTO.RichiestaRISDTO data);
        int DeleteRichiestaRISById(string esamidid);

        string SendMirthRequest(string richidid);
        int ChangeHL7StatusAndMessageAll(string richidid, string hl7_stato, string hl7_msg = null);
        List<DTO.EsameDTO> ChangeHL7StatusAndMessageEsami(List<string> esamids, string hl7_stato, string hl7_msg = null);
        DTO.RichiestaRISDTO ChangeHL7StatusAndMessageRichiestaRIS(string richidid, string hl7_stato, string hl7_msg = null);
        bool ValidatePres(DTO.RichiestaRISDTO pres, ref string errorString);
        bool ValidateRadios(List<DTO.EsameDTO> radios, ref string errorString);
    }
}