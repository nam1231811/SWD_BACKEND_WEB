using EduConnect.DTO;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StudentInfo>> GetAllStudentsAsync()
        {
            var students = await _repo.GetAllStudentsAsync();

            return students.Select(s => new StudentInfo
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                ClassId = s.ClassId,
            });
        }

        public async Task<List<StudentInfo>> GetByClassIdAsync(string classId)
        {
            var students = await _repo.GetByClassIdAsync(classId);
            return students.Select(s => new StudentInfo
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                ClassId = s.ClassId,
                DateOfBirth = s.DateOfBirth
            }).ToList();
        }
    }
}
