using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Classroom
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassroomController(IClassService classService)
        {
            _classService = classService;
        }

        //tim theo id
        [HttpGet("classroom")]
        public async Task<IActionResult> GetClassById(String ClassId)
        {
            var result = await _classService.GetByIdAsync(ClassId);
            if (result == null)
            {
                return NotFound("Classroom is not available");
            }
            return Ok(result);
        }

        //tao class moi
        [HttpPost("create")]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreate dto)
        {
            await _classService.CreateClassAsync(dto);
            return Ok("Classroom created");
        }

        //delete class 
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClass(string ClassId)
        {
            await _classService.DeleteClassAsync(ClassId);
            return Ok("Classroom deleted");
        }

        //update class
        [HttpPut("update")]
        public async Task<IActionResult> UpdateClass([FromBody] ClassCreate dto)
        {
            await _classService.UpdateClassAsync(dto);
            return Ok("Classroom updated");
        }
    }
}
