using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend.Repositories
{
    public class ResumeEditorRepository : IResumeEditorRepository
    {
        private AppDbContext _context;
        public ResumeEditorRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<int> CreateEmptyResume(int userId)
        {
            var profile = await GetUserProfileAsync(userId);
            var resume = new Resume()
            {
                UserId = userId,
                ProfileId = profile.Id,
            };
            await _context.Resume.AddAsync(resume);
            await _context.SaveChangesAsync();
            return resume.Id;
        }

        public async Task<ResponseResult<int>> SendAboutInfo(int userId, int resumeId, AboutDto aboutDto)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            resume.About = aboutDto.About;
            resume.Hobbies = aboutDto.Hobbies;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendProjects(int userId, int resumeId, List<ProjectDto> projectDtos)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            foreach (var projectDto in projectDtos)
            {
                var project = new Project()
                {
                    Description = projectDto.Description,
                    Link = projectDto.Link,
                    Title = projectDto.Title,
                    Resume = resume,
                    ResumeId = resumeId
                };
                await _context.Project.AddAsync(project);
            }

            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resumeId);
        }

        public async Task<ResponseResult<int>> SendExperience(int userId, int resumeId, ExperienceDto experienceDto)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            resume.Experience = experienceDto.Info;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendAchievements(int userId, int resumeId, List<AchievementDto> achievementDtos)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            foreach (var achievementDto in achievementDtos)
            {
                var achievement = new Achievement()
                {
                    Description = achievementDto.Info,
                    Resume = resume,
                    ResumeId = resumeId
                };
                await _context.Achievement.AddAsync(achievement);
            }
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resumeId);
        }

        public async Task<ResponseResult<int>> SendEducation(int userId, int resumeId, List<EducationDto> educationDtos)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            foreach (var educationDto in educationDtos)
            {
                var education = new Education()
                {
                    Level = educationDto.Level,
                    Place = educationDto.Place,
                    Specialization = educationDto.Specialization,
                    StartYear = educationDto.StartYear,
                    EndYear = educationDto.EndYear,
                    Resume = resume,
                    ResumeId = resumeId
                };
                await _context.AddAsync(education);
            }
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resumeId);
        }

        public async Task<ResponseResult<int>> SendSkills(int userId, int resumeId, string skills)
        {
            var resume = await GetResume(resumeId, userId);
            if (resume is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["ResumeError"] = "Резюме с таким Id не существует"}
                });
            resume.Skills = skills;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        private async Task<Resume> GetResume(int resumeId, int userId)
        {
            return await _context.Resume.FirstOrDefaultAsync(r => r.Id == resumeId && r.UserId == userId);
        }

        private async Task<User> GetUserAsync(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        private async Task<Profile> GetUserProfileAsync(int userId)
        {
            return await _context.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
