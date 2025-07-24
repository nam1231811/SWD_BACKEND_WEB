﻿using EduConnect.DTO;
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
        private readonly IWebHostEnvironment _env;

        public ParentController(IParentService parentService, IWebHostEnvironment env)
        {
            _parentService = parentService;
            _env = env;
        }

        //lay thong tin hoc sinh
        // GET: /api/parents/students
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentInfo(string email)
        {
            var result = await _parentService.GetStudentInfoAsync(email);
            return Ok(result);
        }

        //lay thong tin profile
        // GET: /api/parents/profile
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
                return NotFound("Can not found the information");
            }
            return Ok(parentProfile);
        }

        //chinh sua profile
        // PUT: /api/parents/profile
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateParentProfile dto)
        {

            //lay mail trong token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email not found in token");

            var result = await _parentService.UpdateProfileAsync(email, dto, dto.ImageUrl);

            if (!result) 
            { 
                return NotFound("Không tìm thấy phụ huynh"); 
            }

            //tra OK
            return Ok("Profile updated successfully");
        }

    }
}
