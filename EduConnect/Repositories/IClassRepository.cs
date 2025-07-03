using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IClassRepository
    {
        Task<Classroom> GetByIdAsync(string ClassId);
        Task<List<Classroom>> GetByTeacherIdAsync(string teacherId);
        Task CreateClassAsync(Classroom dto);
        Task UpdateClassAsync(Classroom dto);
        Task DeleteClassAsync(string ClassId);
    }
}
