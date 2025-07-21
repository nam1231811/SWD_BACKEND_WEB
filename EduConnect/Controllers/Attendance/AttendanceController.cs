using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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


        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourseId(string courseId)
        {
            var result = await _attendanceService.GetByCourseIdAsync(courseId);
            return Ok(result);
        }


        //tao attendance
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateAttendance([FromBody] List<AttendanceCreate> dto)
        {
            var result = await _attendanceService.AddAttendanceAsync(dto);
            if (result == null)
                return Ok("Đã lưu điểm danh. Không gửi được notification (thiếu studentId hoặc fcmToken).");

            return Ok(new
            {
                Message = "Lưu điểm danh thành công. Đã gửi thông báo.",
                FirebaseMessageId = result
            });
        }

        //update attendance
        [Authorize(Roles = "Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceCreate dto)
        {
            await _attendanceService.UpdateAttendanceAsync(dto);
            return Ok("Updated");
        }

        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClassId(string classId)
        {
            var result = await _attendanceService.GetByClassIdAsync(classId);
            return Ok(result);
        }

        //delete
        [Authorize(Roles = "Teacher")]
        [HttpDelete("course/{courseId}")]
        public async Task<IActionResult> DeleteAllByCourseId(string courseId)
        {
            var result = await _attendanceService.DeleteAllByCourseIdAsync(courseId);
            if (!result) return NotFound("Can found Attendance by CourseId");
            return Ok("Delete Sucessfull.");
        }

        
    }
}
