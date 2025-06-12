using EduConnect.Models;
using EduConnect.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace EduConnect.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<User>();
        }

        //dang nhap
        public async Task<string?> LoginAsync(Login request)
        {
            //tim user theo email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null) 
            {
                return null;
            }

            //hash lai pasword de so sanh
            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            ////sai mat khau tra ve null
            //if (user.PasswordHash != passwordHash)
            //    return null;

            return _jwtService.GenerateToken(user);
        }

        //dang ki nguoi dung moi
        public async Task<string> RegisterAsync(Register request)
        {
            //check mail da co hay chua
            var existUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existUser != null)
            {
                throw new Exception("Email already exits.");
            }      

            //tao user moi
            var user = new User
            {
                Email = request.Email,
            };

            //hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            //luu vao database
            await _userRepository.AddUserAsync(user);
            return "User registered successfully.";
        }
    }
}
