using FSTW_backend.Dto.InternshipDto;
using FSTW_backend.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("/admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminPanelController : ControllerBase
    {
        private IAdminService _service;
        private HttpClient _httpClient;
        public AdminPanelController(IAdminService service, IHttpClientFactory factory)
        {
            _service = service;
            _httpClient = factory.CreateClient();
        }

        [HttpPost("create/multiply")]
        public async Task<IActionResult> CreateInternship([FromBody] List<RequestInternshipDto> internshipDtos)
        {
            foreach (var internshipDto in internshipDtos)
            {
                var response = await _service.CreateInternship(internshipDto);
                if (!response.Successed)
                    return BadRequest(response.Errors);
            }
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInternship([FromBody] RequestInternshipDto internshipDto)
        {
            var response = await _service.CreateInternship(internshipDto);
            if (response.Successed)
                return Ok();
            return BadRequest(response.Errors);
        }

        [HttpGet("internship/{internshipId}")]
        public async Task<IActionResult> GetInternship([FromRoute] int internshipId)
        {
            var response = await _service.GetInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpGet("all_internships")]
        public async Task<IActionResult> GetInternship([FromQuery] string filterParam)
        {
            var response = await _service.GetAllInternships(filterParam);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPost("edit_internship/{internshipId}")]
        public async Task<IActionResult> EditInternship([FromRoute] int internshipId, [FromBody] RequestInternshipDto internshipDto)
        {
            var response = await _service.EditInternship(internshipId, internshipDto);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpDelete("internship/{internshipId}/remove")]
        public async Task<IActionResult> DeleteInternship([FromRoute] int internshipId)
        {
            var response = await _service.DeleteInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPut("internship/{internshipId}/archive")]
        public async Task<IActionResult> ArchiveInternship([FromRoute] int internshipId)
        {
            var response = await _service.ArchiveInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPut("internship/{internshipId}/unarchive")]
        public async Task<IActionResult> UnarchiveInternship([FromRoute] int internshipId)
        {
            var response = await _service.UnarchiveInternship(internshipId);
            if (response.Successed)
                return Ok(response.Value);
            return BadRequest(response.Errors);
        }

        [HttpPost("hh")]
        public async Task<IActionResult> AddInternshipsFromHh()
        {
            await _service.AddVacanciesFromHh(_httpClient);
            return Ok();
        }

        [HttpGet("parsing")]
        public async Task<IActionResult> ParseSites()
        {
            var response = await _httpClient.GetAsync("https://kontur.ru/education/programs/intern/backendall");
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
