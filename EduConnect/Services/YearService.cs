using EduConnect.DTO;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class YearService : IYearService
    {
        private readonly IYearRepository _yearRepository;

        public YearService(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<GetYear?> GetSchoolYearById(string id)
        {
           var year = await _yearRepository.GetSchoolYearById(id);
            if (year == null)
            {
                return null;
            }
            return new GetYear
            {
                SchoolYearID = year.SchoolYearID,
                StartDate = year.StartDate,
                EndDate = year.EndDate,
                Status = year.Status,
            };
        }
    }
}
