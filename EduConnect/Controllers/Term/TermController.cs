using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Term
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly ITermService _termService;

        public TermController (ITermService termService) 
        { 
            _termService = termService;
        }

        //get tem id
        [HttpGet("Term")]
        public async Task<IActionResult> GetTermById(String TermId)
        {
            var result = await _termService.GetTermById(TermId);
            if (result == null)
            {
                return NotFound("Term is not available");
            }
            return Ok(result);
        }

        //tao term moi
        [HttpPost("create")]
        public async Task<IActionResult> CreateTerm([FromBody] TermCreated dto)
        {
            await _termService.CreateTerm(dto);
            return Ok("Term created");
        }

        //update term
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTerm([FromBody] TermCreated dto)
        {
            await _termService.UpdateTerm(dto);
            return Ok("Term updated");
        }
    }
}
