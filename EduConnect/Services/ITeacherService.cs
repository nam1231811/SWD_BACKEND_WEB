using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface ITeacherService
    {
        Task<Teacher> GetByIdAsync(string id);
        Task<PagedResult<Teacher>> GetAsync(string? search, string? sortBy, string? sortDirection, string? status, int page, int pageSize);
        Task<Teacher> CreateAsync(CreateTeacher dto);
        Task<bool> UpdateAsync(string id, UpdateTeacher dto);
        Task<bool> DeleteAsync(string id);
    }
}
