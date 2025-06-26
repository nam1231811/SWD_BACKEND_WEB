using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance?> GetAttendanceByIdAsync(string id);
        Task AddAttendanceAsync(Attendance attendance);
        Task UpdateAttendanceAsync(Attendance attendance);
    }
}
