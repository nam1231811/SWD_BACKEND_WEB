using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Subject
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService) 
        {
            _subjectService = subjectService;
        }

        //tim sub theo id
        [HttpGet("subject")]
        public async Task<IActionResult> GetSubjectById(String SubjectId)
        {
            var result = await _subjectService.GetByIdAsync(SubjectId);
            if (result == null)
            {
                return NotFound("Subject is not available");
            }
            return Ok(result);
        }

        //tao subject
        [HttpPost("create")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreated dto)
        {
            await _subjectService.CreateSubjectAsync(dto);
            return Ok("Subject created");
        }

        //update subject
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubject([FromBody] SubjectCreated dto)
        {
            await _subjectService.UpdateSubjectAsync(dto);
            return Ok("Subject updated");
        }

        //delete Subject
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSubject(string SubjectId)
        {
            await _subjectService.DeleteSubjectAsync(SubjectId);
            return Ok("Subject deleted");
        }

    }
}
