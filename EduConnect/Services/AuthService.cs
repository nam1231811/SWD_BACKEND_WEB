using EduConnect.DTO;
using EduConnect.Entities;
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
        private readonly IStudentRepository _studentRepository;

        public AuthService(IUserRepository userRepository, JwtService jwtService, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<User>();
            _studentRepository = studentRepository;
        }

        //dang nhap
        public async Task<LoginResponse?> LoginAsync(Login request)
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

            var token = _jwtService.GenerateToken(user);
            return new LoginResponse
            {
                Token = token,
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
            };
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
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = $"{request.FirstName} {request.LastName}",
                Role = "Parent",
                IsActive = true, // ve sau co them xac thuc
                CreateAt = DateTime.UtcNow,
            };

            //upload file
            if (request.UserImage != null && request.UserImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.UserImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.UserImage.CopyToAsync(stream);
                }

                user.UserImage = "/uploads/" + fileName;
            }

            //hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            //luu vao database
            await _userRepository.AddUserAsync(user);

            //tao tk parent moi
            var parent = new Parent
            {
                UserId = user.UserId,
            };
            await _userRepository.AddParentAsync(parent);

            //add parent vao student
            if (request.StudentIds != null && request.StudentIds.Count > 0)
            {
                foreach (var studentId in request.StudentIds)
                {
                    var student = await _studentRepository.GetByIdAsync(studentId);
                    if (student == null)
                    {
                        throw new Exception($"Student ID '{studentId}' not found.");
                    }

                    student.ParentId = parent.ParentId;
                    await _studentRepository.UpdateAsync(student);
                }
            }
            return "User registered successfully.";
        }

        public async Task<bool> ResetPasswordAsync(ResetPassword dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null) return false;

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.NewPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
