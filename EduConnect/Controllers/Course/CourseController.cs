using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }

    // Tạo tiết học mới

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseCreate dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { courseId = id, message = "Course created" });
    }

    // GET: api/course/teacher/{teacherId}
    [Authorize(Roles = "Teacher")]
    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetByTeacherId(string teacherId)
    {
        var result = await _service.GetByTeacherIdAsync(teacherId);
        return Ok(result); // trả về List<CourseDetail>
    }

    // GET: api/course/class/{classId}
    [Authorize(Roles = "Teacher,Parent")]
    [HttpGet("class/{classId}")]
    public async Task<IActionResult> GetByClassId(string classId)
    {
        var result = await _service.GetByClassIdAsync(classId);
        return Ok(result);
    }

    // PUT: api/course/status
    [Authorize(Roles = "Teacher")]
    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateCourse dto)
    {
        await _service.UpdateStatusAsync(dto);
        return Ok(new { message = "Cập nhật trạng thái thành công" });
    }
}
