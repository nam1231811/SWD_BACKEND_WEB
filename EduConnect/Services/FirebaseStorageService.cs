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
            var path = Path.Combine(env.ContentRootPath, "chat-app-5396e-firebase-adminsdk-kq3dv-90a99ca70c.json");

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

            var uploadResult = await _storageClient.UploadObjectAsync(
                bucket: _bucket,
                objectName: fileName,
                contentType: file.ContentType,
                source: stream,
                options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead }
            );

            return $"https://firebasestorage.googleapis.com/v0/b/{_bucket}/o/{Uri.EscapeDataString(fileName)}?alt=media";
        }
    }
}
