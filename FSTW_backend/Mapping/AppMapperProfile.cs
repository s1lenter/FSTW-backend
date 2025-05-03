using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace FSTW_backend.Mapping
{
    public class AppMapperProfile : AutoMapper.Profile
    {
        public AppMapperProfile()
        {
            CreateMap<PersonalCabinetDto, Profile>().ReverseMap();
            CreateMap<UserRegisterRequestDto, User>().ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => HashPassword(src.Password)));
        }

        private string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null, password); // Можно передать null вместо user
        }
    }
}
