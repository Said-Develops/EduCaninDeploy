using System.ComponentModel.DataAnnotations;

namespace EduCanin.Models.Entities
{
    public class DogCourseSession
    {

        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public int? CourseSessionId { get; set; }
        public CourseSession CourseSession { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? TrainerComment { get; set; }
        public bool WasPresent { get; set; }
    }
}
