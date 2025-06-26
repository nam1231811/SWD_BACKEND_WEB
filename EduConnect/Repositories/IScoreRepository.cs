using EduConnect.Entities;

namespace EduConnect.Repositories;

public interface IScoreRepository
{
    Task<Score> AddAsync(Score score);
    Task<Score?> GetByIdAsync(string scoreId);
    Task UpdateAsync(Score score);
}
