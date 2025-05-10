using FSTW_backend.Models;

namespace FSTW_backend.Dto.ResumeDto
{
    public class AllResumeInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Vk { get; set; }
        public string Telegram { get; set; }
        public string GitHub { get; set; }
        public string Linkedin { get; set; }
        public string Email { get; set; }

        public string Hobbies { get; set; }
        public string About { get; set; }

        public string Experience { get; set; }

        public string Skills { get; set; }

        public List<EducationDto> Educations { get; set; }

        public List<ProjectDto> Projects { get; set; }

        public List<AchievementDto> Achievements { get; set; }
    }
}
