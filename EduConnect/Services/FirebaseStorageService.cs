using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace EduConnect.Services
{
    public class FirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucket;

        public FirebaseStorageService(IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "chat-app-5396e-firebase-adminsdk-kq3dv-a89a4347b2.json");

            // Khởi tạo Firebase app nếu chưa có
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(path)
                });
            }

            _storageClient = StorageClient.Create(GoogleCredential.FromFile(path));
            _bucket = "chat-app-5396e.appspot.com"; // 🔴 Đổi lại bằng tên bucket của bạn
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName = $"images/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using var stream = file.OpenReadStream();

            var uploadResult = await _storageClient.UploadObjectAsync(_bucket, fileName, file.ContentType, stream);
            return $"https://firebasestorage.googleapis.com/v0/b/{_bucket}/o/{Uri.EscapeDataString(fileName)}?alt=media";
        }
    }
}
