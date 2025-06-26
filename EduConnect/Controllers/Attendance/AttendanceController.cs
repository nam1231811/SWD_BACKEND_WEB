using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        //tim theo id
        [HttpGet("getid")]
        public async Task<IActionResult> GetAttendanceById(string id)
        {
            var data = await _attendanceService.GetAttendanceByIdAsync(id);
            if (data == null) 
            { 
                return NotFound(); 
            }
            return Ok(data);
        }

        //tao attendance
        [HttpPost("create")]
        public async Task<IActionResult> CreateAttendance([FromBody] AttendanceCreate dto)
        {
            await _attendanceService.AddAttendanceAsync(dto);
            return Ok("Created successfully");
        }

        //update attendance
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceCreate dto)
        {
            await _attendanceService.UpdateAttendanceAsync(dto);
            return Ok("Updated");
        }
    }
}
