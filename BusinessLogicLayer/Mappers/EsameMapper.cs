using AutoMapper;
using System.Collections.Generic;

namespace BusinessLogicLayer.Mappers
{
    public class EsameMapper
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IBLL.DTO.EsameDTO EsamMapper(IDAL.VO.EsameVO raw)
        {
            IBLL.DTO.EsameDTO esam = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IDAL.VO.EsameVO, IBLL.DTO.EsameDTO>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<IBLL.DTO.EsameDTO>(raw);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }
        public static List<IBLL.DTO.EsameDTO> EsamMapper(List<IDAL.VO.EsameVO> raws)
        {
            List<IBLL.DTO.EsameDTO> res = null;

            if (raws != null)
            {
                res = new List<IBLL.DTO.EsameDTO>();
                foreach (IDAL.VO.EsameVO raw in raws)
                {
                    res.Add(EsamMapper(raw));
                }
            }

            return res;
        }
        public static IDAL.VO.EsameVO EsamMapper(IBLL.DTO.EsameDTO data)
        {
            IDAL.VO.EsameVO esam = null;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<IBLL.DTO.EsameDTO, IDAL.VO.EsameVO>());
                Mapper.AssertConfigurationIsValid();
                esam = Mapper.Map<IDAL.VO.EsameVO>(data);
            }
            catch (AutoMapperConfigurationException ex)
            {
                log.Error(string.Format("AutoMapper Configuration Error!\n{0}", ex.Message));
            }
            catch (AutoMapperMappingException ex)
            {
                log.Error(string.Format("AutoMapper Mapping Error!\n{0}", ex.Message));
            }

            return esam;
        }
        public static List<IDAL.VO.EsameVO> EsamMapper(List<IBLL.DTO.EsameDTO> dtos)
        {
            List<IDAL.VO.EsameVO> res = null;

            if (dtos != null)
            {
                res = new List<IDAL.VO.EsameVO>();
                foreach (IBLL.DTO.EsameDTO raw in dtos)
                {
                    res.Add(EsamMapper(raw));
                }
            }

            return res;
        }

    }
}
