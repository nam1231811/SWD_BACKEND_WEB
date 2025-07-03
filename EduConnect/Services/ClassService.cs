using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repo;

        public ClassService(IClassRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateClassAsync(ClassCreate dto)
        {
            var classroom = new Classroom
            {
                ClassId = Guid.NewGuid().ToString(),
                ClassName = dto.ClassName,
                TeacherId = dto.TeacherId,
            };
            await _repo.CreateClassAsync(classroom);
        }

        public async Task DeleteClassAsync(string classId)
        {
            await _repo.DeleteClassAsync(classId);
        }

        // Trả về ClassProfile
        public async Task<ClassCreate> GetByIdAsync(string classId)
        {
            var cls = await _repo.GetByIdAsync(classId);
            if (cls == null) return null;

            return new ClassCreate
            {
                ClassId = cls.ClassId,
                ClassName = cls.ClassName,
                TeacherId = cls.TeacherId
            };
        }

        public async Task UpdateClassAsync(ClassCreate dto)
        {
            var classroom = await _repo.GetByIdAsync(dto.ClassId);
            if (classroom == null) return;

            classroom.ClassName = dto.ClassName;
            classroom.TeacherId = dto.TeacherId;
            await _repo.UpdateClassAsync(classroom);
        }

        // Trả danh sách lớp theo giáo viên chủ nhiệm
        public async Task<List<ClassProfile>> GetByTeacherIdAsync(string teacherId)
        {
            var list = await _repo.GetByTeacherIdAsync(teacherId);
            return list.Select(cls => new ClassProfile
            {
                ClassId = cls.ClassId,
                ClassName = cls.ClassName,
                TeacherId = cls.TeacherId
            }).ToList();
        }
    }
}
