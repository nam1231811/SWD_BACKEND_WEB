using EduConnect.DTO;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace EduConnect.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("edunotification-d5eb2-firebase-adminsdk-fbsvc-1d468d18a3.json")
                });
            }
        }
        public async Task<string?> SendAttendanceNotificationAsync(AttendanceNotification dto)
        {
            var message = new Message
            {
                Token = dto.FcmToken,
                Notification = new Notification
                {
                    Title = dto.Title,
                    Body = dto.Body
                }
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }
    }
}
