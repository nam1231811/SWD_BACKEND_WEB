using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance?> GetAttendanceByIdAsync(string id);
        Task AddAttendanceAsync(List<Attendance> attendance);
        Task UpdateAttendanceAsync(Attendance attendance);
        Task<IEnumerable<Attendance>> GetByCourseIdAsync(string courseId);
        Task<List<Attendance>> GetByClassIdAsync(string classId);
        Task<bool> DeleteAllByCourseIdAsync(string courseId);
        Task<TeacherFcmToken?> GetTeacherFcmByAttendanceIdAsync(string atId);
    }
}

