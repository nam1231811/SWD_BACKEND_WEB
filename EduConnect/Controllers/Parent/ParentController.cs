using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduConnect.Controllers.Parent
{
    [Authorize(Roles = "Parent")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        //lay thong tin hoc sinh
        [HttpGet("student/{email}")]
        public async Task<IActionResult> GetStudentInfo(string email)
        {
            var result = await _parentService.GetStudentInfoAsync(email);
            return Ok(result);
        }

        //lay thong tin profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetParentsInfo()
        {
            //tim mail trong token de xac nhan
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            //k thay trả unauthorize
            if (string.IsNullOrEmpty(email))
            { 
                return Unauthorized("Not found email in token");
            }
            var parentProfile = await _parentService.GetProfileAsync(email);

            if (parentProfile == null)
            {
                return NotFound("Can not the information");
            }
            return Ok(parentProfile);
        }

        //chinh sua profile
        [HttpPut("profile/{email}")]
        public async Task<IActionResult> UpdateProfile(string email, [FromBody] UpdateParentProfile dto)
        {
            //update thong tin
            var success = await _parentService.UpdateProfileAsync(email, dto);
            //loi tra not found
            if (!success)
            {
                return NotFound("Parent not found");
            }
            //tra OK
            return Ok("Profile updated successfully");
        }

    }
}
