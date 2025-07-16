using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ITeacherRepository _eacherRepository;
        private readonly INotificationService _notificationService;

        public AttendanceService(IAttendanceRepository attendanceRepository, ITeacherRepository eacherRepository, INotificationService notificationService)
        {
            _attendanceRepository = attendanceRepository;
            _eacherRepository = eacherRepository;
            _notificationService = notificationService;
        }

        public async Task<string?> AddAttendanceAsync(List<AttendanceCreate> dto)
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

            var studentId = dto.FirstOrDefault()?.StudentId;
            if (string.IsNullOrEmpty(studentId))
            {
                return null;
            }
            var fcmInfo = await _eacherRepository.GetTeacherFcmByStudentIdAsync(studentId);
            if (fcmInfo == null || string.IsNullOrEmpty(fcmInfo.FcmToken)) { 
                return null;
            }
            var notification = new AttendanceNotification
            {
                FcmToken = fcmInfo.FcmToken,
                Title = "Thông báo điểm danh",
                Body = "Một học sinh vừa được điểm danh.",
                Platform = fcmInfo.Platform
            };

            return await _notificationService.SendAttendanceNotificationAsync(notification);
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

        public async Task<bool> DeleteAllByCourseIdAsync(string courseId)
        {
            return await _attendanceRepository.DeleteAllByCourseIdAsync(courseId);
        }


    }
}
