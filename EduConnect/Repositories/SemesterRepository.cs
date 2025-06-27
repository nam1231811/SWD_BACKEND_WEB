using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories;

public class SemesterRepository : ISemesterRepository
{
    private readonly AppDbContext _context;

    public SemesterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Semester>> GetAllAsync()
    {
        return await _context.Semesters.ToListAsync();
    }

    public async Task<Semester?> GetByIdAsync(string id)
    {
        return await _context.Semesters.FindAsync(id);
    }

    public async Task<Semester> AddAsync(Semester semester)
    {
        _context.Semesters.Add(semester);
        await _context.SaveChangesAsync();
        return semester;
    }

    public async Task UpdateAsync(Semester semester)
    {
        _context.Semesters.Update(semester);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Semester semester)
    {
        _context.Semesters.Remove(semester);
        await _context.SaveChangesAsync();
    }
}
