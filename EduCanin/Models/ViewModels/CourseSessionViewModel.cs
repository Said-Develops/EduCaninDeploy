namespace EduCanin.Models.ViewModels
{
    public class CourseSessionViewModel
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string CoachName { get; set; }
        public int SpotsLeft { get; set; }
        public bool IsFull { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
