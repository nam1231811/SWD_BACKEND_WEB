using EduConnect.Models;
using EduConnect.Models.User;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //dang ki nguoi dung moi
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //dang nhap
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
                return Unauthorized(new { error = "Invalid email or password." });

            return Ok(new { token });
        }
    }
}
