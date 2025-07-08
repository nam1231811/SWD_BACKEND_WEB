using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IStudentService
    {
        Task<List<StudentInfo>> GetByClassIdAsync(string classId);
        Task<IEnumerable<StudentInfo>> GetAllStudentsAsync();
    }
}
