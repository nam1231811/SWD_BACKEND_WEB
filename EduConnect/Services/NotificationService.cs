using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task CreateAsync(NotificationCreate dto)
        {
            var noti = new Notification
            {
                NotiId = Guid.NewGuid().ToString(),
                TeacherId = dto.TeacherId,
                Title = dto.Title,
                Description = dto.Description,
                ClassId = dto.ClassId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepository.AddAsync(noti);
        }

        public async Task DeleteAsync(string notiId)
        {
            await _notificationRepository.DeleteAsync(notiId);
        }

        //tim toan bo noti trong class
        public async Task<List<Notification>> GetByClassIdAsync(string classId)
        {
            var list = await _notificationRepository.GetByClassIdAsync(classId);
            return list.Select(n => new Notification
            {
                NotiId = n.NotiId,
                Title = n.Title,
                Description = n.Description,
                ClassId = n.ClassId,
                TeacherId = n.TeacherId,
                StartDate = n.StartDate,
                EndDate = n.EndDate,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        //tim noti by id
        public async Task<Notification?> GetByIdAsync(string notiId)
        {
            var n = await _notificationRepository.GetByIdAsync(notiId);
            if (n == null) return null;
            return new Notification
            {
                NotiId = n.NotiId,
                Title = n.Title,
                Description = n.Description,
                ClassId = n.ClassId,
                TeacherId = n.TeacherId,
                StartDate = n.StartDate,
                EndDate = n.EndDate,
                CreatedAt = n.CreatedAt
            };
        }
    }
}
