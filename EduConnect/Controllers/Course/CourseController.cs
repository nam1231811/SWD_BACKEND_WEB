using EduConnect.DTO;
using EduConnect.Services;
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
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseCreate dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(new { courseId = id, message = "Course created" });
    }

    // Tìm theo ID
    [HttpGet("{CourseId}")]
    public async Task<IActionResult> GetById(string CourseId)
    {
        var result = await _service.GetByIdAsync(CourseId);
        if (result == null) return NotFound("Course not found");

        return Ok(result);
    }
}
