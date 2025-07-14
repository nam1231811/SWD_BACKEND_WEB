using EduConnect.Data;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public class ChatBotLogRepository : IChatBotLogRepository
    {
        private readonly AppDbContext _context;

        public ChatBotLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatBotLog> CreateLogAsync(string parentId, string messageText, string responseText)
        {
            var message = new Message
            {
                MessageText = messageText,
                ResponseText = responseText,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var log = new ChatBotLog
            {
                ChatId = Guid.NewGuid().ToString(),
                MessageId = message.MessageId,
                ParentId = parentId,
                CreatedAt = DateTime.Now,
                Title = messageText.Length > 50 ? messageText[..50] + "..." : messageText,
                Message = message
            };

            _context.ChatBotLogs.Add(log);
            await _context.SaveChangesAsync();

            return log;
        }
    }
}
