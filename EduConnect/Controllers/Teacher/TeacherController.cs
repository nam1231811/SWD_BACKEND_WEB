using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IScoreService _scoreService;

    public TeacherController(ITeacherService teacherService, IScoreService scoreService)
    {
        _teacherService = teacherService;
        _scoreService = scoreService;
    }

    // GET /api/Teacher/{id}
    [HttpGet("{TeacherId}")]
    public async Task<IActionResult> GetById(string id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        if (teacher == null) return NotFound("Teacher not found");
        return Ok(teacher);
    }

    // POST /api/Teacher
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacher dto)
    {
        var id = await _teacherService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { teacherId = id });
    }

    // PUT /api/Teacher/{id}
    [HttpPut]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateTeacher dto)
    {
        var updated = await _teacherService.UpdateAsync(id, dto);
        return updated ? Ok("Teacher updated") : NotFound("Teacher not found");
    }

    // DELETE /api/Teacher/{id}
    [HttpDelete("{TeacherId}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _teacherService.DeleteAsync(id);
        return deleted ? Ok("Teacher deleted") : NotFound("Teacher not found");
    }

    // POST /api/Teacher/{teacherId}/score  -> Nhập điểm
    [HttpPost("{TeacherId}/score")]
    public async Task<IActionResult> EnterScore(string teacherId, [FromBody] ScoreCreate dto)
    {
        var scoreId = await _scoreService.CreateScoreAsync(dto);
        return CreatedAtAction(nameof(EnterScore), new { teacherId }, new { scoreId });
    }

    // PUT /api/Teacher/{teacherId}/score/{scoreId}  -> Sửa điểm
    [HttpPut("{TeacherId}/score/{ScoreId}")]
    public async Task<IActionResult> UpdateScore(string teacherId, string scoreId, [FromBody] UpdateScore dto)
    {
        var updated = await _scoreService.UpdateScoreAsync(scoreId, dto);
        return updated ? Ok("Score updated") : NotFound("Score not found");
    }
}
