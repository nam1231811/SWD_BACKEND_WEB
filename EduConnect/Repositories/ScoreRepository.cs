using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories;

public class ScoreRepository : IScoreRepository
{
    private readonly AppDbContext _context;

    public ScoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Score> AddAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    public async Task<Score?> GetByIdAsync(string scoreId)
    {
        return await _context.Scores.FindAsync(scoreId);
    }

    public async Task UpdateAsync(Score score)
    {
        _context.Scores.Update(score);
        await _context.SaveChangesAsync();
    }
}
