using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Attendance
{
    [Authorize(Roles = "Teacher")]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(string id)
        {
            var data = await _attendanceService.GetAttendanceByIdAsync(id);
            if (data == null) 
            { 
                return NotFound(); 
            }
            return Ok(data);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourseId(string courseId)
        {
            var result = await _attendanceService.GetByCourseIdAsync(courseId);
            return Ok(result);
        }

        //tao attendance
        [HttpPost]
        public async Task<IActionResult> CreateAttendance([FromBody] List<AttendanceCreate> dto)
        {
            await _attendanceService.AddAttendanceAsync(dto);
            return Ok("Created successfully");
        }

        //update attendance
        [HttpPut]
        public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceCreate dto)
        {
            await _attendanceService.UpdateAttendanceAsync(dto);
            return Ok("Updated");
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClassId(string classId)
        {
            var result = await _attendanceService.GetByClassIdAsync(classId);
            return Ok(result);
        }

        //delete
        [HttpDelete("course/{courseId}")]
        public async Task<IActionResult> DeleteAllByCourseId(string courseId)
        {
            var result = await _attendanceService.DeleteAllByCourseIdAsync(courseId);
            if (!result) return NotFound("Can found Attendance by CourseId");
            return Ok("Delete Sucessfull.");
        }
    }
}
