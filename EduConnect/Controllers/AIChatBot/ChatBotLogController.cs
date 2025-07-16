using EduConnect.DTO;
using EduConnect.Repositories;
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
        private readonly IChatBotLogRepository _chatBotLogRepository;

        public ChatBotLogController(
            GroqService groqService,
            StudentStatusService studentStatusService,
            IChatBotLogRepository chatBotLogRepository)
        {
            _groqService = groqService;
            _studentStatusService = studentStatusService;
            _chatBotLogRepository = chatBotLogRepository;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest dto)
        {
            // 🧠 Lấy thông tin học sinh + phụ huynh từ DB
            var studentContext = await _studentStatusService.GenerateFullStudentContextAsync(dto.ParentId);

            // 🗨️ Tạo prompt gửi lên Groq API
            var fullPrompt = $@"
Dữ liệu hệ thống cung cấp:

{studentContext}

Câu hỏi của phụ huynh:
""{dto.MessageText}""
";

            // 🤖 Gửi lên Groq để nhận phản hồi AI
            var reply = await _groqService.AskAsync(fullPrompt);

            // 📝 Ghi log lại câu hỏi - câu trả lời
            await _chatBotLogRepository.CreateLogAsync(dto.ParentId, dto.MessageText, reply);

            // 📦 Trả về cho client
            return Ok(new { reply });
        }
    }
}
