using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IClassService
    {
        Task<ClassCreate> GetByIdAsync(string ClassId);
        Task CreateClassAsync(ClassCreate dto);
        Task UpdateClassAsync(ClassCreate dto);
        Task DeleteClassAsync(string ClassId);
    }
}
