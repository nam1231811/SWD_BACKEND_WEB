using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EduConnect.Services
{
    public class GroqService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        // ✅ System prompt chi tiết, rõ vai trò
        private const string SystemPrompt = @"
Bạn là trợ lý giáo vụ của hệ thống quản lý trường học. Nhiệm vụ của bạn là:

1. Trả lời các câu hỏi của phụ huynh học sinh bằng tiếng Việt, ngắn gọn, đúng trọng tâm và lịch sự.
2. Chỉ sử dụng dữ liệu đã được cung cấp bên dưới (thông tin học sinh, điểm số, thời khóa biểu, giáo viên...).
3. Nếu câu hỏi liên quan đến:
   - 🗓️ Hôm nay học gì: trích thông tin từ phần '📅 Tình hình học tập hôm nay'.
   - 📆 Lịch học: sử dụng phần '📚 Thời khóa biểu trong tuần' hoặc '📆 Lịch học sắp tới'.
   - 📊 Điểm số: từ phần '📊 Điểm số'.
   - 👨‍🏫 Giáo viên chủ nhiệm: từ phần '👨‍🏫 Giáo viên chủ nhiệm'.
   - 👪 Thông tin phụ huynh: từ phần '👨‍👩‍👧‍👦 Thông tin phụ huynh'.
4. Nếu không có thông tin phù hợp, hãy lịch sự trả lời rằng hiện chưa có dữ liệu.
5. Không tự suy diễn hoặc tạo thêm thông tin ngoài những gì được cung cấp.

Câu trả lời cần chính xác, rõ ràng và không dư thừa.
";

        // ✅ Giới hạn chiều dài prompt (tính bằng ký tự, không phải token)
        private const int MaxPromptLength = 6000;

        public GroqService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["Groq:ApiKey"];
        }

        public async Task<string> AskAsync(string fullPrompt)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            // ✅ Cắt bớt prompt nếu quá dài
            var truncatedPrompt = TruncatePrompt(fullPrompt, MaxPromptLength);

            var request = new
            {
                model = "llama3-70b-8192",
                messages = new[]
                {
                    new { role = "system", content = SystemPrompt },
                    new { role = "user", content = truncatedPrompt }
                },
                temperature = 0.7,
                max_tokens = 1000
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Groq error ({response.StatusCode}): {json}");

            dynamic result = JsonConvert.DeserializeObject<dynamic>(json);
            return result.choices[0].message.content.ToString();
        }

        // ✅ Hàm cắt bớt nội dung nếu quá dài (ưu tiên lấy phần cuối prompt)
        private string TruncatePrompt(string prompt, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return prompt;

            return prompt.Length <= maxLength
                ? prompt
                : prompt.Substring(prompt.Length - maxLength);
        }
    }
}

