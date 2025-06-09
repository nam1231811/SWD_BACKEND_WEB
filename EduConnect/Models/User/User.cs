namespace EduConnect.Models.User
{
    public class User
    {

        public string Id { get; set; } = Guid.NewGuid().ToString(); //tao id tu dong trong db
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;
    }
}
