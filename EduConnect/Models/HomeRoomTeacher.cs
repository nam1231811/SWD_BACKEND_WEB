namespace EduConnect.Models
{
    public class HomeRoomTeacher
    {
        public int HomeRoomTeacherID { get; set; }
        public int UserID { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
