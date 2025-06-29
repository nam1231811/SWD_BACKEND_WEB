using EduConnect.DTO;
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
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized(new { error = "Invalid email or password." });

            return Ok(result);
        }

        //reset mat khau
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPasswordAsync(dto);
            if (!result)
                return NotFound("Email not found");

            return Ok("Password reset sucessfully.");
        }
    }
}
