using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByIdAsync(string id);
        Task<Teacher> AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);

        // Nhập điểm và sửa điểm
        Task<Score> AddScoreAsync(Score score);
        Task<bool> UpdateScoreAsync(string scoreId, decimal newScore);
    }
}
