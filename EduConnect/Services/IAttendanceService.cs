using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceCreate?> GetAttendanceByIdAsync(string id);
        Task AddAttendanceAsync(List<AttendanceCreate> dto);
        Task UpdateAttendanceAsync(AttendanceCreate dto);
        Task<IEnumerable<AttendanceCreate>> GetByCourseIdAsync(string courseId);
    }
}
