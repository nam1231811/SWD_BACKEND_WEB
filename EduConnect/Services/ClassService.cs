using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repository;

        public ClassService(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateClassAsync(ClassCreate dto)
        {
            var classroom = new Classroom
            {
                ClassId = Guid.NewGuid().ToString(),
                ClassName = dto.ClassName,
                TeacherId = dto.TeacherId,
            };
            await _repository.CreateClassAsync(classroom);
        }

        public async Task DeleteClassAsync(string ClassId)
        {
            await _repository.DeleteClassAsync(ClassId);
        }

        public async Task<ClassCreate> GetByIdAsync(string ClassId)
        {
            var classroom = await _repository.GetByIdAsync(ClassId);
            if (classroom == null)
            {
                return null;
            }
            return new ClassCreate
            {
                ClassId = classroom.ClassId,
                ClassName = classroom.ClassName,
                TeacherId = classroom.TeacherId,
            };
        }

        public async Task UpdateClassAsync(ClassCreate dto)
        {
            var classroom = await _repository.GetByIdAsync(dto.ClassId);
            if (classroom == null)
            {
                return;
            }

            classroom.ClassName = dto.ClassName;
            classroom.TeacherId = dto.TeacherId;
            await _repository.UpdateClassAsync(classroom);
        }
    }
}
