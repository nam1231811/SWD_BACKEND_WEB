using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetByClassIdAsync(string classId);
        Task<Notification?> GetByIdAsync(string notiId);
        Task AddAsync(Notification notification);
        Task DeleteAsync(string notiId);
    }
}
