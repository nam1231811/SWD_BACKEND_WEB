using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //tim theo class id, liet ke toan bo noti trong class do
        //GET: /api/notifications?classId=xyz
        [HttpGet]
        public async Task<IActionResult> GetByClass(string classId)
        {
            var notis = await _notificationService.GetByClassIdAsync(classId);
            return Ok(notis);
        }

        //tim noti dua vao id
        // GET: /api/notifications/{NotiId}
        [HttpGet("{NotiId}")]
        public async Task<IActionResult> GetById(string NotiId)
        {
            var noti = await _notificationService.GetByIdAsync(NotiId);
            //false trả về not found,true trả về info noti
            return noti == null ? NotFound() : Ok(noti);
        }

        //tao notification
        // POST: /api/notifications
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotificationCreate dto)
        {
            await _notificationService.CreateAsync(dto);
            return Ok(new { message = "Notification created successfully." });
        }

        //xoa notification
        // DELETE: /api/notifications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _notificationService.DeleteAsync(id);
            return Ok(new { message = "Notification deleted successfully." });
        }
    }
}
