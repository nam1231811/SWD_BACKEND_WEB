using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IParentRepository
    {
        Task<Parent?> GetByEmailAsync(string email);
        Task UpdateParentProfileAsync(User user);
        Task<Parent?> GetParentWithStudentsAsync(string email);
       
    }
}
