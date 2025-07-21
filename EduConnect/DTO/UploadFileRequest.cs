using Microsoft.AspNetCore.Http;

namespace EduConnect.DTO
{
    public class UploadFileRequest
    {
        public IFormFile File { get; set; }
    }
}
