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
            try
            {
                var result = await _attendanceService.AddAttendanceAsync(dto);

                if (result == null)
                {
                    // 👉 Khi không gửi được FCM hoặc đã comment đoạn gửi FCM
                    return Ok(new
                    {
                        Message = "✅ Điểm danh đã lưu thành công (chưa gửi thông báo)."
                    });
                }

                // 👉 Khi lưu và gửi FCM thành công
                return Ok(new
                {
                    Message = "✅ Lưu điểm danh thành công và đã gửi thông báo!",
                    FirebaseMessageId = result
                });
            }
            catch (Exception ex)
            {
                // 👉 Log lỗi và trả kết quả an toàn
                Console.WriteLine($"[ERROR] Attendance POST: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "❌ Lỗi khi lưu điểm danh hoặc gửi thông báo.",
                    Error = ex.Message
                });
            }
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
