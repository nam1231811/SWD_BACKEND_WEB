using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IYearRepository
    {
        Task<SchoolYear?> GetSchoolYearById(string id);
    }
}
