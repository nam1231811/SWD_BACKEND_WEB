using EduConnect.DTO;
using EduConnect.Services;
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

        // Tìm học kỳ theo ID
        // GET: /api/semesters/{SemesterId}
        [HttpGet("{SemesterId}")]
        public async Task<IActionResult> GetSemesterById(string SemesterId)
        {
            var result = await _semesterService.GetByIdAsync(SemesterId);
            if (result == null)
            {
                return NotFound("Semester not found");
            }
            return Ok(result);
        }

        // Tạo mới học kỳ
        // POST: /api/semesters
        [HttpPost]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterCreate dto)
        {
            await _semesterService.CreateAsync(dto);
            return Ok("Semester created");
        }

        // Cập nhật học kỳ
        // PUT: /api/semesters/{{SemesterId}}
        [HttpPut("{SemesterId}")]
        public async Task<IActionResult> UpdateSemester(string SemesterId, [FromBody] UpdateSemester dto)
        {
            var success = await _semesterService.UpdateAsync(SemesterId, dto);
            if (!success) return NotFound("Semester not found");

            return Ok("Semester updated");
        }

        // Xoá học kỳ
        // DELETE: /api/semesters/{SemesterId}
        [HttpDelete("{SemesterId}")]
        public async Task<IActionResult> DeleteSemester(string SemesterId)
        {
            var success = await _semesterService.DeleteAsync(SemesterId);
            if (!success) return NotFound("Semester not found");

            return Ok("Semester deleted");
        }
    }
}
