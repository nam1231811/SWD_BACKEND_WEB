using EduConnect.DTO;
using EduConnect.Entities;
using Microsoft.AspNetCore.Identity.Data;

namespace EduConnect.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(Login request);
        Task<LoginResponse> GoogleLoginAsync(GoogleAuthSettings request);
        Task<string> RegisterAsync(Register request);

        Task<bool> ResetPasswordAsync(ResetPassword dto);
    }
}
