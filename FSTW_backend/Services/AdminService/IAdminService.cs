using FSTW_backend.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Services.Admin
{
    public interface IAdminService
    {
        public Task<ResponseResult<int>> CreateInternship(RequestInternshipDto internshipDto);

        public Task<ResponseResult<InternshipDto>> GetInternship(int internshipId);

        public Task<ResponseResult<List<InternshipDto>>> GetAllInternships(string filterParam);

        public Task<ResponseResult<int>> EditInternship(int internshipId, RequestInternshipDto internshipDto);

        public Task<ResponseResult<int>> DeleteInternship(int internshipId);

        public Task<ResponseResult<int>> ArchiveInternship(int internshipId);

        public Task<ResponseResult<int>> UnarchiveInternship(int internshipId);

        public Task AddVacanciesFromHh(HttpClient httpClient);
    }
}
