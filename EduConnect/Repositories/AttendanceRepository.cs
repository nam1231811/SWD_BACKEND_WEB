using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _appDbContext;

        public AttendanceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAttendanceAsync(Attendance attendance)
        {
           _appDbContext.Attendances.Add(attendance);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Attendance?> GetAttendanceByIdAsync(string id)
        {
            return await _appDbContext.Attendances
                .Include(a => a.Course)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.AtID == id);
        }

        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            _appDbContext.Attendances.Update(attendance);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
