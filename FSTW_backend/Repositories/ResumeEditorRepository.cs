using FSTW_backend.Dto;
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

        public async Task<ResponseResult<int>> SendAboutInfo(Resume resume, AboutDto aboutDto)
        {
            resume.About = aboutDto.About;
            resume.Hobbies = aboutDto.Hobbies;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendProjects(Resume resume, List<Project> projects)
        {
            await _context.Project.AddRangeAsync(projects);
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendExperience(Resume resume, string experience)
        {
            resume.Experience = experience;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendAchievements(Resume resume, List<Achievement> achievements)
        {
            await _context.AddRangeAsync(achievements);
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendEducation(Resume resume, List<Education> educations)
        {
            await _context.AddRangeAsync(educations);
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<ResponseResult<int>> SendSkills(Resume resume, string skills)
        {
            resume.Skills = skills;
            await _context.SaveChangesAsync();
            return ResponseResult<int>.Success(resume.Id);
        }

        public async Task<Resume> GetCurrentResume(int userId, int resumeId)
        {
            return await _context.Resume.FirstOrDefaultAsync(r => r.Id == resumeId && r.UserId == userId);
        }

        public async Task DeleteResume(Resume resume)
        {
            _context.Resume.Remove(resume);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeOnceResumeInfo(Resume resume, ChangeResumeInfoDto changeResumeInfoDto)
        {
            resume.About = changeResumeInfoDto.About;
            resume.Hobbies = changeResumeInfoDto.Hobbies;
            resume.Experience = changeResumeInfoDto.Experience;
            resume.Skills = changeResumeInfoDto.Skills;

            _context.SaveChangesAsync();
        }

        public List<Project> GetProjects(int resumeId)
        {
            return _context.Project.Where(p => p.ResumeId == resumeId).ToList();
        }



        public List<Education> GetEducations(int resumeId)
        {
            return _context.Education.Where(e => e.ResumeId == resumeId).ToList();
        }

        public List<Achievement> GetAchievements(int resumeId)
        {
            return _context.Achievement.Where(a => a.ResumeId == resumeId).ToList();
        }

        public async Task RemoveProjects(List<Project> projects)
        {
            _context.Project.RemoveRange(projects);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAchievements(List<Achievement> achievements)
        {
            _context.Achievement.RemoveRange(achievements);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEducations(List<Education> educations)
        {
            _context.Education.RemoveRange(educations);
            await _context.SaveChangesAsync();
        }

        public async Task<Profile> GetUserProfileAsync(int userId)
        {
            return await _context.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
