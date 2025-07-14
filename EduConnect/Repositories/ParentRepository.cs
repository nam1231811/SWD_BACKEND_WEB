using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly AppDbContext _context;

        public ParentRepository(AppDbContext context)
        {
            _context = context;
        }

    

        //tim 
        public async Task<Parent?> GetByEmailAsync(string email)
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(s => s.Class)
                .FirstOrDefaultAsync(p => p.User != null && p.User.Email == email);
        }

        public async Task<Parent?> GetParentWithStudentsAsync(string userId)
        {
            return await _context.Parents
            .Include(p => p.Students)
            .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        //update thong tin va luu
        public async Task UpdateParentProfileAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
