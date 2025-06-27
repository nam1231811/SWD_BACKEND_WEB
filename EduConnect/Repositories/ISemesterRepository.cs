using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface ISemesterRepository
    {
        Task<List<Semester>> GetAllAsync();
        Task<Semester?> GetByIdAsync(string id);
        Task<Semester> AddAsync(Semester semester);
        Task UpdateAsync(Semester semester);
        Task DeleteAsync(Semester semester);
    }
}
