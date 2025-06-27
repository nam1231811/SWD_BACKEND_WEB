using EduConnect.Data;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface ITermRepository
    {
        Task<Term?> GetTermById (string termId);
        Task CreateTerm (Term term);
        Task UpdateTerm (Term term);

    }
}
