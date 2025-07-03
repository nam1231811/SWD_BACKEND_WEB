using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(string studentId);
        Task UpdateAsync(Student student);
        Task<List<Student>> GetByClassIdAsync(string classId);
    }
}
