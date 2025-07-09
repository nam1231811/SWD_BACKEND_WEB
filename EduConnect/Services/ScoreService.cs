using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services;

public class ScoreService : IScoreService
{
    private readonly IScoreRepository _scoreRepo;

    public ScoreService(IScoreRepository scoreRepo)
    {
        _scoreRepo = scoreRepo;
    }

    public async Task<string> CreateScoreAsync(ScoreCreate dto)
    {
        var score = new Score
        {
            ScoreId = Guid.NewGuid().ToString(),
            StudentId = dto.StudentId,
            SemeId = dto.SemeId,
            Type = dto.Type,
            Score1 = dto.Score1
        };

        var result = await _scoreRepo.AddAsync(score);
        return result.ScoreId;
    }

    public async Task<bool> UpdateScoreAsync(string scoreId, UpdateScore dto)
    {
        var score = await _scoreRepo.GetByIdAsync(scoreId);
        if (score == null) return false;

        score.Score1 = dto.Score1;
        score.Type = dto.Type;
        await _scoreRepo.UpdateAsync(score);
        return true;
    }
}
