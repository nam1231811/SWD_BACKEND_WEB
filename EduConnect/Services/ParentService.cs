using EduConnect.Data;
using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;

        public ParentService(IParentRepository parentRepository, AppDbContext appDbContext, IUserRepository userRepository)
        {
            _parentRepository = parentRepository;
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }

        public async Task<ParentProfile?> GetProfileAsync(string email)
        {
            var parent = await _parentRepository.GetByEmailAsync(email);
            if (parent == null || parent.User == null)
            { 
                return null; 
            }

            return new ParentProfile
            {
                ParentId = parent.ParentId,
                UserId = parent.UserId,
                FullName = parent.User.FullName,
                Email = parent.User.Email,
                PhoneNumber = parent.User.PhoneNumber,
                UserImage = parent.User.UserImage
            };
        }

        public async Task<List<StudentInfo>> GetStudentInfoAsync(string email)
        {
            //tim parent theo userId
            var parent = await _parentRepository.GetByEmailAsync(email);
            if (parent == null || parent.Students == null)
            {
                return new List<StudentInfo>();
            }

            //lay thong tin hoc sinh
            return parent.Students.Select(s => new StudentInfo
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                ClassId = s.Class?.ClassId
            }).ToList();
        }

        public async Task<bool> UpdateProfileAsync(string email, UpdateParentProfile dto, string? imageUrl)
        {
            //tim parent theo userId
            var parent = await _parentRepository.GetByEmailAsync(email);
            if (parent == null || parent.User == null)
            {
                return false;
            }
            //update thong tin can sua
            parent.User.PhoneNumber = dto.PhoneNumber;
            parent.User.FirstName = dto.FirstName;
            parent.User.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                parent.User.UserImage = imageUrl;
            }
            //luu vao db
            await _userRepository.UpdateAsync(parent.User);
            return true;
        }
    }
}
