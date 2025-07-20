using EduConnect.DTO;
using EduConnect.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly FirebaseStorageService _firebaseService;

        public UploadController(FirebaseStorageService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] UploadFileRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("File không hợp lệ");

            var url = await _firebaseService.UploadFileAsync(request.File);
            return Ok(new { imageUrl = url });
        }
    }
}
