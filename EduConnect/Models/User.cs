

namespace EduConnect.Models
{
    public class User
    {

        public string Id { get; set; } = Guid.NewGuid().ToString(); //tao id tu dong trong db
        public string? FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime? CreateAt { get; set; } = DateTime.MinValue;

        // Navigation
        public ICollection<Parent> Parents { get; set; }
        public ICollection<HomeRoomTeacher> HomeRoomTeachers { get; set; }
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; }

    }
}
