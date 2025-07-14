using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IChatBotLogRepository
    {
        Task<ChatBotLog> CreateLogAsync(string parentId, string messageText, string responseText);
    }
}
