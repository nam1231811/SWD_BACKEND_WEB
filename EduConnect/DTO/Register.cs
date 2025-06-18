using System.ComponentModel.DataAnnotations;

namespace EduConnect.DTO
{
    public class Register
    {
        [Required]public string? Email { get; set; } = string.Empty;
        [Required]public string? Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string>? StudentIds { get; set; }
    }
}
