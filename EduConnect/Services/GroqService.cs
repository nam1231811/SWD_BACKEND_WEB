using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EduConnect.Services
{
    public class GroqService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public GroqService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["Groq:ApiKey"]; // ✅ Lấy từ appsettings.json
        }

        public async Task<string> AskAsync(string fullPrompt)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var request = new
            {
                model = "llama3-70b-8192",
                messages = new[]
                {
                new { role = "system", content = "Bạn là trợ lý giáo vụ..." },
                new { role = "user", content = fullPrompt }
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
    }
}
