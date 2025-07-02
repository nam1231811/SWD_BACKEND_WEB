using EduConnect.Entities;
namespace EduConnect.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task AddParentAsync(Parent parent);
        Task UpdateAsync(User user);
    }
}
