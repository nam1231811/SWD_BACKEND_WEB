using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Utils;

namespace EduConnect.Services;

public class GroqReportService
{
    private readonly ReportGenerationService _reportGen;
    private readonly GroqReportService _groqReportService;

    public GroqReportService(ReportGenerationService reportGen, GroqReportService groqReportService)
    {
        _reportGen = reportGen;
        _groqReportService = groqReportService;
    }
    public async Task<string> SendPromptAsync(string prompt)
    {
        // Ví dụ gọi Groq API hoặc model AI
        var client = new HttpClient();
        // ... cấu hình headers, body v.v...
        var response = await client.PostAsync("https://api.groq.com/...", new StringContent(prompt));
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GenerateReportTextAsync(ReportBot input)
    {
        var content = await _reportGen.GenerateReportContentAsync(input);

        var prompt = $"""
        Bạn là một cố vấn học tập. Dưới đây là dữ liệu thống kê:

        {content}

        Hãy viết một đoạn báo cáo chính thức ngắn gọn, phù hợp gửi cho phụ huynh để tổng kết tình hình học tập lớp học.
        """;

        return await _groqReportService.SendPromptAsync(prompt);
    }
}
