using EduConnect.DTO;
using EduConnect.Services;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScoreCreate dto) // Nhập điểm
    {
        var id = await _scoreService.CreateScoreAsync(dto);
        return CreatedAtAction(nameof(Create), new { id }, new { scoreId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateScore dto) // Cập nhật điểm
    {
        var success = await _scoreService.UpdateScoreAsync(id, dto);
        if (!success) return NotFound();

        return NoContent();
    }
}
