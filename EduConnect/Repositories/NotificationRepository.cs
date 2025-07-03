
using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext  _appDbContext;

        public NotificationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string?> GetFcmTokenByUserIdAsync(string UserId)
        {
            return await _appDbContext.Teachers
           .Where(t => t.UserId == UserId && !string.IsNullOrEmpty(t.FcmToken))
           .Select(t => t.FcmToken)
           .FirstOrDefaultAsync();
        }
    }
}
