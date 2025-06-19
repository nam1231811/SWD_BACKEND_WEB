using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface ITeacherService
    {
        Task<TeacherDTO> GetByIdAsync(string id);
        Task<PagedResult<TeacherDTO>> GetAsync(string? search, string? sortBy, string? sortDirection, string? status, int page, int pageSize);
        Task<TeacherDTO> CreateAsync(CreateTeacherDTO dto);
        Task<bool> UpdateAsync(string id, UpdateTeacherDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}
