using EduConnect.Data;
using EduConnect.DTO;
using EduConnect.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context; // DbContext để truy cập DB
        private readonly IMapper _mapper; // AutoMapper để ánh xạ DTO ↔ Entity

        public TeacherService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DTO.TeacherProfile> GetByIdAsync(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id); // Tìm giáo viên theo ID
            return teacher == null ? null : _mapper.Map<DTO.TeacherProfile>(teacher); // Nếu không có thì trả null
        }

        public async Task<PagedResult<DTO.TeacherProfile>> GetAsync(string? search, string? sortBy, string? sortDirection, string? status, int page, int pageSize)
        {
            var query = _context.Teachers.AsQueryable(); // Truy vấn bảng Teachers

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Status.Contains(search)); // Tìm theo status chứa từ khóa
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status == status); // Lọc theo trạng thái
            }

            // Sắp xếp theo điều kiện được truyền
            if (!string.IsNullOrEmpty(sortBy))
            {
                bool descending = sortDirection?.ToLower() == "desc";

                query = sortBy.ToLower() switch
                {
                    "starttime" => descending ? query.OrderByDescending(t => t.StartTime) : query.OrderBy(t => t.StartTime),
                    "endtime" => descending ? query.OrderByDescending(t => t.EndTime) : query.OrderBy(t => t.EndTime),
                    "status" => descending ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status),
                    _ => query.OrderBy(t => t.TeacherId)
                };
            }

            var total = await query.CountAsync(); // Tổng số kết quả
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(); // Phân trang
            var mapped = _mapper.Map<IEnumerable<DTO.TeacherProfile>>(items); // Map sang DTO

            return new PagedResult<DTO.TeacherProfile> { TotalItems = total, Items = mapped }; // Trả về kết quả
        }

        public async Task<DTO.TeacherProfile> CreateAsync(CreateTeacher dto)
        {
            var teacher = _mapper.Map<Entities.Teacher>(dto); // Map DTO sang entity
            _context.Teachers.Add(teacher); // Thêm vào DbSet
            await _context.SaveChangesAsync(); // Lưu thay đổi
            return _mapper.Map<DTO.TeacherProfile>(teacher); // Trả về DTO
        }

        public async Task<bool> UpdateAsync(string id, UpdateTeacher dto)
        {
            var teacher = await _context.Teachers.FindAsync(id); // Tìm giáo viên
            if (teacher == null) return false;

            _mapper.Map(dto, teacher); // Map DTO vào entity
            await _context.SaveChangesAsync(); // Lưu thay đổi
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var teacher = await _context.Teachers.FindAsync(id); // Tìm giáo viên
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher); // Xóa
            await _context.SaveChangesAsync(); // Lưu
            return true;
        }
    }

}
