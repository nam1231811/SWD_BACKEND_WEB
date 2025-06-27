using EduConnect.DTO;

namespace EduConnect.Services
{
    public interface ITermService
    {
        Task<TermCreated?> GetTermById (string termId);
        Task CreateTerm(TermCreated term);
        Task UpdateTerm(TermCreated term);
    }
}
