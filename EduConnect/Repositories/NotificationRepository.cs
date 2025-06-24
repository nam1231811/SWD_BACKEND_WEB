using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _appDbContext;

        public NotificationRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Notification notification)
        {
            _appDbContext.Notifications.Add(notification);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string notiId)
        {
            var noti = await _appDbContext.Notifications.FindAsync(notiId);
            if (noti != null)
            {
                _appDbContext.Notifications.Remove(noti);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Notification>> GetByClassIdAsync(string classId)
        {
            return await _appDbContext.Notifications
            .Include(n => n.Class)
            .Include(n => n.Teacher)
            .Where(n => n.ClassId == classId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(string notiId)
        {
            return await _appDbContext.Notifications
            .Include(n => n.Class)
            .Include(n => n.Teacher)
            .FirstOrDefaultAsync(n => n.NotiId == notiId);
        }
    }
}
