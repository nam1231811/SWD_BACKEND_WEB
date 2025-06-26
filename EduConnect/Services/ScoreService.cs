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

    public async Task<string> CreateScoreAsync(ScoreCreate dto) // Hàm này sẽ tạo một điểm số mới cho học sinh
    {
        var score = new Score
        {
            ScoreId = Guid.NewGuid().ToString(),
            StudentId = dto.StudentId,
            SubjectId = dto.SubjectId,
            Score1 = dto.Score1
        };

        var result = await _scoreRepo.AddAsync(score);
        return result.ScoreId;
    }

    public async Task<bool> UpdateScoreAsync(string scoreId, UpdateScore dto) // Hàm này sẽ cập nhật điểm số của học sinh
    {
        var score = await _scoreRepo.GetByIdAsync(scoreId);
        if (score == null) return false;

        score.Score1 = dto.Score1;
        await _scoreRepo.UpdateAsync(score);
        return true;
    }
}
