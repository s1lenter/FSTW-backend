using FSTW_backend.Dto;
using FSTW_backend.Dto.AuthDto;
using FSTW_backend.Dto.InternshipDto;
using FSTW_backend.Dto.ResumeDto;
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
            CreateMap<EducationDto, Education>().ReverseMap();
            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<AchievementDto, Achievement>().ReverseMap();
            CreateMap<InternshipDto, Internship>().ReverseMap();
            CreateMap<RequestInternshipDto, Internship>().ReverseMap();
        }

        private string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null, password);
        }
    }
}
