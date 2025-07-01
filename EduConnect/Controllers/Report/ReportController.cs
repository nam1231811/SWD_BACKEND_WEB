using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        //tim theo class id, liet ke toan bo noti trong class do
        //GET: /api/notifications?classId=xyz
        [HttpGet]
        public async Task<IActionResult> GetByClass(string classId)
        {
            var reports = await _reportService.GetByClassIdAsync(classId);
            return Ok(reports);
        }

        //tim noti dua vao id
        // GET: /api/notifications/{NotiId}
        [HttpGet("{ReportId}")]
        public async Task<IActionResult> GetById(string ReportId)
        {
            var report = await _reportService.GetByIdAsync(ReportId);
            //false trả về not found,true trả về info noti
            return report == null ? NotFound() : Ok(report);
        }

        //tao notification
        // POST: /api/notifications
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReportCreate dto)
        {
            await _reportService.CreateAsync(dto);
            return Ok(new { message = "Report created successfully." });
        }

        //xoa notification
        // DELETE: /api/notifications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _reportService.DeleteAsync(id);
            return Ok(new { message = "Report deleted successfully." });
        }
    }
}
