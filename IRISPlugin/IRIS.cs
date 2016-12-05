using IBLL.DTO;
using System.Collections.Generic;

namespace IRISPlugin
{
    public interface IRIS
    {
        string ScheduleNewRequest(RichiestaRISDTO esam, List<EsameDTO> radios, ref string errorString);
        MirthResponseDTO SubmitNewRequest(string richid, ref string errorString);
       
        List<EsameDTO> Check4Radios(string richid);
        List<RichiestaRISDTO> Check4Richs(string evenid);

        MirthResponseDTO CancelRequest(string richid, ref string errorString);
        bool CheckIfCancelingIsAllowed(string richid, ref string errorString);
    }
}
