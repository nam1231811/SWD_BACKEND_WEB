using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task AddAttendanceAsync(List<AttendanceCreate> dto)
        {
            var list = dto.Select(dto => new Attendance
            {
                AtID = Guid.NewGuid().ToString(),
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Participation = dto.Participation,
                Note = dto.Note,
                Homework = dto.Homework,
                Focus = dto.Focus
            }).ToList();

            await _attendanceRepository.AddAttendanceAsync(list);
        }

        public async Task<AttendanceCreate?> GetAttendanceByIdAsync(string id)
        {
            var atten = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (atten == null)
            {
                return null;
            }
            return new AttendanceCreate
            {
                AtID = atten.AtID,
                StudentId = atten.StudentId,
                CourseId = atten.CourseId,
                Participation = atten.Participation,
                Note = atten.Note,
                Homework = atten.Homework,
                Focus = atten.Focus,

            };
        }

        public async Task<IEnumerable<AttendanceCreate>> GetByCourseIdAsync(string courseId)
        {
            var attendances = await _attendanceRepository.GetByCourseIdAsync(courseId);
            return attendances.Select(a => new AttendanceCreate
            {
                AtID = a.AtID,
                StudentId = a.StudentId,
                Participation = a.Participation,
                Note = a.Note,
                Homework = a.Homework,
                Focus = a.Focus
            });
        }

        public async Task UpdateAttendanceAsync(AttendanceCreate dto)
        {
            var atten = await _attendanceRepository.GetAttendanceByIdAsync(dto.AtID);
            if (atten == null)
            {
                return;
            }

            atten.Participation = dto.Participation;
            atten.Note = dto.Note;
            atten.Focus = dto.Focus;
            atten.Homework = dto.Homework;

            await _attendanceRepository.UpdateAttendanceAsync(atten);
        }

        public async Task<List<AttendanceProfile>> GetByClassIdAsync(string classId)
        {
            var data = await _attendanceRepository.GetByClassIdAsync(classId);
            return data.Select(a => new AttendanceProfile
            {
                AtID = a.AtID,
                StudentId = a.StudentId,
                CourseId = a.CourseId,
                Participation = a.Participation,
                Note = a.Note,
                Homework = a.Homework,
                Focus = a.Focus
            }).ToList();
        }
    }
}
