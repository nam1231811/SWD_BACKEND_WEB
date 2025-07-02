using EduConnect.DTO;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserInfo
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                CreateAt = u.CreateAt,
                PhoneNumber = u.PhoneNumber
            });
        }
    }
}
