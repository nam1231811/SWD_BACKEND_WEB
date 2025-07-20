using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Report;

[ApiController]
[Route("api/report")]
public class ReportBotController : ControllerBase
{
    private readonly ReportGenerationService _reportGenerationService;

    public ReportBotController(ReportGenerationService reportGenerationService)
    {
        _reportGenerationService = reportGenerationService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateReport([FromBody] ReportBot input)
    {
        if (input == null || string.IsNullOrEmpty(input.TermId) || string.IsNullOrEmpty(input.ClassId))
        {
            return BadRequest("Thiếu thông tin TermId hoặc ClassId.");
        }

        var result = await _reportGenerationService.GenerateReportContentAsync(input);
        return Ok(result);
    }
}
