using EduConnect.Data;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public class TermRepository : ITermRepository
    {
        private readonly AppDbContext _context;

        public TermRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateTerm(Term term)
        {
            _context.Terms.Add(term);
            await _context.SaveChangesAsync();
        }

        public async Task<Term?> GetTermById(string termId)
        {
            return await _context.Terms.FindAsync(termId);
        }

        public async Task UpdateTerm(Term term)
        {
            _context.Terms.Update(term);
            await _context.SaveChangesAsync();
        }
    }
}
