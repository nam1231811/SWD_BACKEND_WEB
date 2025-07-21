using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoreController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    // Nhập điểm
    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateScore([FromBody] ScoreCreate dto)
    {
        var id = await _scoreService.CreateScoreAsync(dto);
        return Ok(new { scoreId = id, message = "Score created" });
    }

    // Cập nhật điểm
    [Authorize(Roles = "Teacher,Admin")]
    [HttpPut("{ScoreId}")]
    public async Task<IActionResult> UpdateScore([FromQuery] string scoreId, [FromBody] UpdateScore dto)
    {
        var success = await _scoreService.UpdateScoreAsync(scoreId, dto);
        if (!success) return NotFound("Score not found");

        return Ok("Score updated");
    }
}
