using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceCreate?> GetAttendanceByIdAsync(string id);
        Task AddAttendanceAsync(AttendanceCreate dto);
        Task UpdateAttendanceAsync(AttendanceCreate dto);
    }
}
