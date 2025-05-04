using FSTW_backend.Models;

namespace FSTW_backend.Dto.ResumeDto
{
    public class AllResumeInfoDto
    {
        public string Hobbies { get; set; }
        public string About { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public List<EducationDto> Educations { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<AchievementDto> Achievements { get; set; }
    }
}
