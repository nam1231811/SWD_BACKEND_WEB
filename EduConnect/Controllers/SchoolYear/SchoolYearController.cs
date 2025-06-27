using EduConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers.SchoolYear
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolYearController : ControllerBase
    {
        private readonly IYearService _yearService;

        public SchoolYearController(IYearService yearService)
        {
            _yearService = yearService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolYearInfo(string id)
        {
            var result = await _yearService.GetSchoolYearById(id);
            return Ok(result);
        }
    }
}
