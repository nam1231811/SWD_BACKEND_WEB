using EduConnect.Models.User;

namespace EduConnect.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
