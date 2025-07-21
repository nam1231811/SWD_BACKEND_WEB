using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Semester
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet]
        public async Task<IActionResult> GetSemester([FromQuery] string? semesterId, [FromQuery] string? schoolYearId)
        {
            if (!string.IsNullOrEmpty(semesterId))
            {
                var result = await _semesterService.GetByIdAsync(semesterId);
                if (result == null) return NotFound("Semester not found");
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(schoolYearId))
            {
                var result = await _semesterService.GetBySchoolYearIdAsync(schoolYearId);
                return Ok(result);
            }

            return BadRequest("You must provide either semesterId or schoolYearId");
        }

        // Tạo mới học kỳ
        // POST: /api/semesters
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterCreate dto)
        {
            await _semesterService.CreateAsync(dto);
            return Ok("Semester created");
        }

        // Cập nhật học kỳ
        // PUT: /api/semesters/{{SemesterId}}
        [Authorize(Roles = "Admin")]
        [HttpPut("{SemesterId}")]
        public async Task<IActionResult> UpdateSemester(string SemesterId, [FromBody] UpdateSemester dto)
        {
            var success = await _semesterService.UpdateAsync(SemesterId, dto);
            if (!success) return NotFound("Semester not found");

            return Ok("Semester updated");
        }

        // Xoá học kỳ
        // DELETE: /api/semesters/{SemesterId}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{SemesterId}")]
        public async Task<IActionResult> DeleteSemester(string SemesterId)
        {
            var success = await _semesterService.DeleteAsync(SemesterId);
            if (!success) return NotFound("Semester not found");

            return Ok("Semester deleted");
        }
    }
}
