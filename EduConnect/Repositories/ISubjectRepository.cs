using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface ISubjectRepository
    {
        Task<Subject> GetByIdAsync(string SubjectId);
        Task CreateSubjectAsync(Subject dto);
        Task UpdateSubjectAsync(Subject dto);
        Task DeleteSubjectAsync(string SubjectId);
    }
}
