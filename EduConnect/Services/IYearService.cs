using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface IYearService
    {
        Task<GetYear?> GetSchoolYearById(string id);
    }
}
