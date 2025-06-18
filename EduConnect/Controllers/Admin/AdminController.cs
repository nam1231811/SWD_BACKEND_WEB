using EduConnect.DTO;
using EduConnect.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPut("assign-role")]
        public async Task<IActionResult> AssignRoleByEmail([FromBody] AssignRole request)
        {
            //tim email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            //check role
            if (request.Role != "Admin" && request.Role != "Teacher")
            {
                return BadRequest("Only 'Admin' or 'Teacher' roles are allowed");
            }

            user.Role = request.Role;
            await _userRepository.UpdateAsync(user);

            return Ok($"Role updated to {request.Role} for user {user.Email}");
        }
    }
}
