using EduConnect.DTO;

namespace EduConnect.Services;

public interface ISemesterService
{
    Task<List<SemesterCreate>> GetAllAsync();
    Task<SemesterCreate?> GetByIdAsync(string id);
    Task<string> CreateAsync(SemesterCreate dto);
    Task<bool> UpdateAsync(string id, UpdateSemester dto);
    Task<bool> DeleteAsync(string id);
    Task<List<SemesterCreate>> GetBySchoolYearIdAsync(string schoolYearId);
}
