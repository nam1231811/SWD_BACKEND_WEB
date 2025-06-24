using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Services
{
    public interface ISubjectService
    {
        Task<SubjectCreated> GetByIdAsync(string SubjectId);
        Task CreateSubjectAsync(SubjectCreated dto);
        Task UpdateSubjectAsync(SubjectCreated dto);
        Task DeleteSubjectAsync(string SubjectId);
    }
}
