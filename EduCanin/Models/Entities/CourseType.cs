namespace EduCanin.Models.Entities
{
    public class CourseType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int AgeMinimal { get; set; }
        public int AgeMaximal { get; set; }
        public int ParticipantsMaximal { get; set; }
        public int MinimalDurationInMinutes { get; set; }
        public int MaximalDurationInMinutes { get; set; }

        public ICollection<Breed> ForbidenBreed { get; set; } = new List<Breed>();
        public ICollection<CourseSession> Sessions { get; set; } = new List<CourseSession>();
    }
}
