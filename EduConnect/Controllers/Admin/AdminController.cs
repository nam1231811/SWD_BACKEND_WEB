using EduConnect.Data;
using EduConnect.DTO;
using EduConnect.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduConnect.Entities;

namespace EduConnect.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _appDbContext;

        public AdminController(IUserRepository userRepository, AppDbContext appDbContext)
        {
            _userRepository = userRepository;
            _appDbContext = appDbContext;
        }


        [HttpPut("users/role")]
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

            if (request.Role == "Teacher")
            {
                
                var existingTeacher = await _appDbContext.Teachers
                    .FirstOrDefaultAsync(t => t.UserId == user.UserId);

                if (existingTeacher == null)
                {
                    var newTeacher = new EduConnect.Entities.Teacher
                    {
                        TeacherId = Guid.NewGuid().ToString(),
                        UserId = user.UserId,
                        Status = "Active", 

                    };

                     _appDbContext.Teachers.Add(newTeacher);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            return Ok($"Role updated to {request.Role} for user {user.Email}");
        }
    }
}
