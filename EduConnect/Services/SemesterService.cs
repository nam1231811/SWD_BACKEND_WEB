using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services;

public class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _repo;

    public SemesterService(ISemesterRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SemesterCreate>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(s => new SemesterCreate
        {
            SemesterName = s.SemesterName,
            StartDate = s.StartDate ?? default,
            EndDate = s.EndDate ?? default,
            SchoolYearID = s.SchoolYearID,
            Status = s.Status
        }).ToList();
    }

    public async Task<SemesterCreate?> GetByIdAsync(string id)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s == null) return null;

        return new SemesterCreate
        {
            SemesterName = s.SemesterName,
            StartDate = s.StartDate ?? default,
            EndDate = s.EndDate ?? default,
            SchoolYearID = s.SchoolYearID,
            Status = s.Status
        };
    }

    public async Task<string> CreateAsync(SemesterCreate dto)
    {
        var semester = new Semester
        {
            SemeId = Guid.NewGuid().ToString(),
            SemesterName = dto.SemesterName,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            SchoolYearID = dto.SchoolYearID,
            Status = dto.Status
        };
        var result = await _repo.AddAsync(semester);
        return result.SemeId;
    }

    public async Task<bool> UpdateAsync(string id, UpdateSemester dto)
    {
        var semester = await _repo.GetByIdAsync(id);
        if (semester == null) return false;

        semester.SemesterName = dto.SemesterName;
        semester.StartDate = dto.StartDate;
        semester.EndDate = dto.EndDate;
        semester.SchoolYearID = dto.SchoolYearID;
        semester.Status = dto.Status;

        await _repo.UpdateAsync(semester);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var semester = await _repo.GetByIdAsync(id);
        if (semester == null) return false;

        await _repo.DeleteAsync(semester);
        return true;
    }
}
