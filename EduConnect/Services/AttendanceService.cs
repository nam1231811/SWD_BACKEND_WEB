using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository  _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task AddAttendanceAsync(AttendanceCreate dto)
        {
            var atten = new Attendance
            {
                AtID = Guid.NewGuid().ToString(),
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Participation = dto.Participation,
                Note = dto.Note,
                Homework = dto.Homework,
                Focus = dto.Focus,
            };
            await _attendanceRepository.AddAttendanceAsync(atten);
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
    }
}
