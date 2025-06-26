using EduConnect.DTO;

namespace EduConnect.Services;

public interface IScoreService
{
    Task<string> CreateScoreAsync(ScoreCreate dto);
    Task<bool> UpdateScoreAsync(string scoreId, UpdateScore dto);
}
