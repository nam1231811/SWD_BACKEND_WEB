using EduConnect.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduConnect.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        //Tao token tu info user
        public string GenerateToken(User user)
        {
            //tao key tu AppSetting
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //mac dinh k co role thi la parent
            var role = string.IsNullOrWhiteSpace(user.Role) ? "Parent" : user.Role;
            //dua thong tin vao token
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

            //tao token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
