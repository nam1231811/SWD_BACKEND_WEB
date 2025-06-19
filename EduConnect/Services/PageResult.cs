namespace EduConnect.Services
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
