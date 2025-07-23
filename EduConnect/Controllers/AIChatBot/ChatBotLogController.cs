using EduConnect.DTO;
using EduConnect.Repositories;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.AIChatBot
{
    [Authorize(Roles = "Parent")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotLogController : ControllerBase
    {
        private readonly GroqService _groqService;
        private readonly QuestionAnalysisService _questionAnalysisService;
        private readonly IChatBotLogRepository _chatBotLogRepository;

        public ChatBotLogController(
            GroqService groqService,
            QuestionAnalysisService questionAnalysisService,
            IChatBotLogRepository chatBotLogRepository)
        {
            _groqService = groqService;
            _questionAnalysisService = questionAnalysisService;
            _chatBotLogRepository = chatBotLogRepository;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest dto)
        {
            try
            {
                // 🎯 Phân tích câu hỏi và tạo prompt gọn nhẹ, phù hợp
                var dynamicPrompt = await _questionAnalysisService.BuildRelevantPromptAsync(dto.ParentId, dto.MessageText);

                // 🤖 Gửi lên Groq AI
                var reply = await _groqService.AskAsync(dynamicPrompt);

                // 📝 Ghi log
                await _chatBotLogRepository.CreateLogAsync(dto.ParentId, dto.MessageText, reply);

                return Ok(new { reply });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
