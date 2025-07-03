using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface INotificationService
    {
        Task<string?> SendAttendanceNotificationAsync(AttendanceNotification dto);
    }
}
