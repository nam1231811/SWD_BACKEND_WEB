using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Teacher
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service; // Inject service

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) =>
            Ok(await _service.GetByIdAsync(id)); // Lấy giáo viên theo ID

        [HttpGet("search")]
        public async Task<IActionResult> Get(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) =>
            Ok(await _service.GetAsync(search, sortBy, sortDirection, status, page, pageSize)); // Lấy danh sách có tìm kiếm, sắp xếp

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacher dto) =>
            Ok(await _service.CreateAsync(dto)); // Tạo mới giáo viên

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTeacher dto) =>
            await _service.UpdateAsync(id, dto) ? Ok() : NotFound(); // Cập nhật

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) =>
            await _service.DeleteAsync(id) ? Ok() : NotFound(); // Xóa
    }

}
