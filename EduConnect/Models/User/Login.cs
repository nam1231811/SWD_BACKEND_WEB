using System.ComponentModel.DataAnnotations;

namespace EduConnect.Models.User
{
    public class Login
    {
        //login by email
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}
