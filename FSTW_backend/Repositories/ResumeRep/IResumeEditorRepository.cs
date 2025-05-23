using FSTW_backend.Dto;
using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories.ResumeRep
{
    public interface IResumeEditorRepository
    {
        public List<Resume> GetAllUserResumes(int userId);

        public Task<int> CreateEmptyResume(int userId);

        public Task<ResponseResult<int>> SendAboutInfo(Resume resume, AboutDto aboutDto);

        public Task<ResponseResult<int>> SendProjects(Resume resume, List<Project> projects);

        public Task<ResponseResult<int>> SendExperience(Resume resume, string experience);

        public Task<ResponseResult<int>> SendAchievements(Resume resume, List<Achievement> achievements);

        public Task<ResponseResult<int>> SendEducation(Resume resume, List<Education> educations);

        public Task<ResponseResult<int>> SendSkills(Resume resume, string skills);

        public List<Project> GetProjects(int resumeId);

        public List<Education> GetEducations(int resumeId);

        public List<Achievement> GetAchievements(int resumeId);

        public Task RemoveProjects(List<Project> projects);

        public Task RemoveAchievements(List<Achievement> achievements);

        public Task RemoveEducations(List<Education> educations);

        public Task<Resume> GetCurrentResume(int userId, int resumeId);

        public Task<Profile> GetUserProfileAsync(int userId);

        public Task<User> GetUserAsync(int userId);

        public Task DeleteResume(Resume resume);

        public Task ChangeOnceResumeInfo(Resume resume, OnlyResumeInfoDto onlyResumeInfoDto);
    }
}
