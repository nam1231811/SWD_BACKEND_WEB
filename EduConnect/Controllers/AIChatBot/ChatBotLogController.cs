using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.AIChatBot
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotLogController : ControllerBase
    {
        private readonly GroqService _groqService;
        private readonly StudentStatusService _studentStatusService;

        public ChatBotLogController(GroqService groqService, StudentStatusService studentStatusService)
        {
            _groqService = groqService;
            _studentStatusService = studentStatusService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest dto)
        {
            var prompt = await _studentStatusService.GenerateFullStudentContextAsync(dto.ParentId);
            var fullPrompt = $"{prompt}\n\nCâu hỏi của phụ huynh: {dto.MessageText}";
            var reply = await _groqService.AskAsync(fullPrompt);

            return Ok(new { reply });
        }
    }
}
