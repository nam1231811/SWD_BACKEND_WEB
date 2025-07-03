using EduConnect.DTO;
using EduConnect.Repositories;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.FireBase
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationRepository notificationRepository, INotificationService notificationService)
        {
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }

        [HttpPost("notify")]
        public async Task<IActionResult> NotifyTeacherAttendance([FromBody] AttendanceNotification dto)
        {
            var token = await _notificationRepository.GetFcmTokenByUserIdAsync(dto.UserId);
            if (string.IsNullOrEmpty(token))
                return NotFound("Teacher không có FCM token");

            dto.FcmToken = token;

            var result = await _notificationService.SendAttendanceNotificationAsync(dto);
            return Ok(new { messageId = result });
        }
    }
}
