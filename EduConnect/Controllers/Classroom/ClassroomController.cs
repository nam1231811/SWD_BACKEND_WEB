using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.Classroom
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassService _classService;

        private readonly IStudentService _studentService;

        public ClassroomController(IClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        //tim theo id
        //[HttpGet("{ClassId}")]
        //public async Task<IActionResult> GetClassById(String ClassId)
        //{
        //    var result = await _classService.GetByIdAsync(ClassId);
        //    if (result == null)
        //    {
        //        return NotFound("Classroom is not available");
        //    }
        //    return Ok(result);
        //}

        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet]
        public async Task<IActionResult> GetClass([FromQuery] string? classId, [FromQuery] string? teacherId)
        {
            if (!string.IsNullOrEmpty(classId))
            {
                var result = await _classService.GetByIdAsync(classId);
                if (result == null)
                    return NotFound("Classroom is not available");

                return Ok(result);
            }

            if (!string.IsNullOrEmpty(teacherId))
            {
                var result = await _classService.GetByTeacherIdAsync(teacherId);
                return Ok(result);
            }

            return BadRequest("Vui lòng cung cấp classId hoặc teacherId");
        }


        //tao class moi
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreate dto)
        {
            await _classService.CreateClassAsync(dto);
            return Ok("Classroom created");
        }

        //delete class 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{ClassId}")]
        public async Task<IActionResult> DeleteClass(string ClassId)
        {
            await _classService.DeleteClassAsync(ClassId);
            return Ok("Classroom deleted");
        }

        //update class
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateClass([FromBody] ClassCreate dto)
        {
            await _classService.UpdateClassAsync(dto);
            return Ok("Classroom updated");
        }

        // GET api/classroom/teacher/{teacherId}
        //[HttpGet("teacher/{teacherId}")]
        //public async Task<IActionResult> GetClassesByTeacherId(string teacherId)
        //{
        //    var result = await _classService.GetByTeacherIdAsync(teacherId);
        //    return Ok(result);
        //}

        // GET /api/Classroom/{classId}/students
        [Authorize(Roles = "Teacher,Parent")]
        [HttpGet("{classId}/students")]
        public async Task<IActionResult> GetStudentsByClassId(string classId)
        {
            var students = await _studentService.GetByClassIdAsync(classId);
            return Ok(students);
        }
    }
}
