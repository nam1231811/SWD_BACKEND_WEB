using EduConnect.Models;

using Microsoft.AspNetCore.Identity.Data;

namespace EduConnect.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(Login request);
        Task<string> RegisterAsync(Register request);
    }
}
