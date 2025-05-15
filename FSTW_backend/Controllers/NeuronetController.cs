using AutoMapper;
using FSTW_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FSTW_backend.Controllers
{
    [ApiController]
    [Route("neuronet")]
    public class NeuronetController : ControllerBase
    {
        private INeuronetService _service;
        private IMapper _mapper;

        public NeuronetController(IMapper mapper, INeuronetService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("/send")]
        public async Task<IActionResult> SendQuestion([FromBody] string question)
        {
            var response = await _service.GetAnswer(question);
            if (!response.Successed)
                return BadRequest();
            return Ok(response.Value);
        }
    }
}
