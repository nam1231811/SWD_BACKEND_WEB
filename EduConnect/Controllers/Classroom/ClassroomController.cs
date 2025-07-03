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

        private readonly IStudentService _studentService;

        public ClassroomController(IClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        //tim theo id
        [HttpGet("{ClassId}")]
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
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreate dto)
        {
            await _classService.CreateClassAsync(dto);
            return Ok("Classroom created");
        }

        //delete class 
        [HttpDelete("{ClassId}")]
        public async Task<IActionResult> DeleteClass(string ClassId)
        {
            await _classService.DeleteClassAsync(ClassId);
            return Ok("Classroom deleted");
        }

        //update class
        [HttpPut]
        public async Task<IActionResult> UpdateClass([FromBody] ClassCreate dto)
        {
            await _classService.UpdateClassAsync(dto);
            return Ok("Classroom updated");
        }

        // GET api/classroom/teacher/{teacherId}
        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetClassesByTeacherId(string teacherId)
        {
            var result = await _classService.GetByTeacherIdAsync(teacherId);
            return Ok(result);
        }

        // GET /api/Classroom/{classId}/students
        [HttpGet("{classId}/students")]
        public async Task<IActionResult> GetStudentsByClassId(string classId)
        {
            var students = await _studentService.GetByClassIdAsync(classId);
            return Ok(students);
        }
    }
}
