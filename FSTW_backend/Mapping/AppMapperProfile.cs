using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend.Mapping
{
    public class AppMapperProfile : AutoMapper.Profile
    {
        public AppMapperProfile()
        {
            CreateMap<PersonalCabinetDto, Profile>();
        }
    }
}
