namespace EduConnect.Repositories
{
    public interface INotificationRepository
    {
        Task<string?> GetFcmTokenByUserIdAsync(string UserId);

    }
}
