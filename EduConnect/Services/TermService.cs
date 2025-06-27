using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class TermService : ITermService
    {
        private readonly ITermRepository _termRepository;
        public TermService(ITermRepository termRepository) 
        {
            _termRepository = termRepository;
        }
        public async Task CreateTerm(TermCreated term)
        {
            var terms = new Term
            {
                TermID = Guid.NewGuid().ToString(),
                StartTime = term.StartTime,
                EndTime = term.EndTime,
                CreatedAt = DateTime.Now,
                NotiID = term.NotiID,
            };
            await _termRepository.CreateTerm(terms);
        }

        public async Task<TermCreated?> GetTermById(string termId)
        {
            var terms = await _termRepository.GetTermById(termId);
            if (terms == null)
            {
                return null;
            }
            return new TermCreated
            {
                TermID = terms.TermID,
                StartTime = terms.StartTime,
                CreatedAt = DateTime.Now,
                EndTime = terms.EndTime,
                NotiID = terms.NotiID,
            };
        }

        public async Task UpdateTerm(TermCreated term)
        {
            var terms = await _termRepository.GetTermById(term.TermID);
            if (terms == null)
            {
                return;
            }
            terms.StartTime = term.StartTime;
            terms.NotiID = term.NotiID;
            term.EndTime = term.EndTime;
            await _termRepository.UpdateTerm(terms);

        }
    }
}
