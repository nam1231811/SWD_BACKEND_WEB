using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers;

[Authorize(Roles = "Teacher")]
[ApiController]
[Route("api/Teacher")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _service;
    public TeacherController(ITeacherService service) => _service = service;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var teacher = await _service.GetByUserIdAsync(userId);
        if (teacher == null) return NotFound();
        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacher dto)
    {
        var teacher = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByUserId), new { userId = teacher.UserId }, teacher);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(string userId, [FromBody] UpdateTeacher dto)
    {
        await _service.UpdateAsync(userId, dto);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        await _service.DeleteAsync(userId);
        return NoContent();
    }

    [HttpPut("{userId}/fcm")]
    public async Task<IActionResult> UpdateFcmToken(string userId, [FromBody] UpdateFcmToken dto)
    {
        await _service.UpdateFcmTokenAsync(userId, dto);
        return NoContent();
    }

    [HttpGet("parent-profile/{studentId}")]
    public async Task<IActionResult> GetParentProfile(string studentId)
    {
        var parentProfile = await _service.GetParentProfileByStudentIdAsync(studentId);
        if (parentProfile == null) return NotFound(new { message = "Khong tim thay phu huynh cua hoc sinh" });
        return Ok(parentProfile);
    }
}


