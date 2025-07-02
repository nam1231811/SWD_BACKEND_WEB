using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();
    }
}
