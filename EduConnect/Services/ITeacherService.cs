using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface ITeacherService
    {
        Task<TeacherProfile> GetByIdAsync(string id);
        Task<PagedResult<TeacherProfile>> GetAsync(string? search, string? sortBy, string? sortDirection, string? status, int page, int pageSize);
        Task<TeacherProfile> CreateAsync(CreateTeacher dto);
        Task<bool> UpdateAsync(string id, UpdateTeacher dto);
        Task<bool> DeleteAsync(string id);
    }
}
