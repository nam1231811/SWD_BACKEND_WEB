using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetByClassIdAsync(string classId);
        Task<Notification?> GetByIdAsync(string notiId);
        Task CreateAsync(NotificationCreate dto);
        Task DeleteAsync(string notiId);
    }
}
