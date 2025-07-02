namespace EduConnect.DTO
{
    public class UserInfo
    {
        public String UserId { get; set; } 

        public string? Role { get; set; }

        public bool? IsActive { get; set; }

        public string? FullName { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
