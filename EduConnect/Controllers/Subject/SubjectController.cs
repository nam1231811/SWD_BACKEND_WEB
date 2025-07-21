using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
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
        // GET: /api/subjects/{SubjectId}'
        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet("{SubjectId}")]
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
        // POST: /api/subjects
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreated dto)
        {
            await _subjectService.CreateSubjectAsync(dto);
            return Ok("Subject created");
        }

        //update subject
        // PUT: /api/subjects/{SubjectId}
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody] SubjectCreated dto)
        {
            await _subjectService.UpdateSubjectAsync(dto);
            return Ok("Subject updated");
        }

        //delete Subject
        // DELETE: /api/subjects/{SubjectId}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{SubjectId}")]
        public async Task<IActionResult> DeleteSubject(string SubjectId)
        {
            await _subjectService.DeleteSubjectAsync(SubjectId);
            return Ok("Subject deleted");
        }

    }
}
