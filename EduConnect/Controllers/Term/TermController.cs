using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Teacher")]
        [HttpGet("{TermId}")]
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
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateTerm([FromBody] TermCreated dto)
        {
            var termId = await _termService.CreateTerm(dto);
            return Ok(new { TermId = termId });
        }

        //update term
        [Authorize(Roles = "Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateTerm([FromBody] TermCreated dto)
        {
            await _termService.UpdateTerm(dto);
            return Ok("Term updated");
        }
    }
}
