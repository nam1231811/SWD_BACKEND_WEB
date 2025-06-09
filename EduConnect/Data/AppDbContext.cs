using EduConnect.Models.User;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
