using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCanin.Models.Entities
{
    public class CourseSession
    {
        [Key]
        public int Id { get; set; }

        public int DurationInMinutes { get; set; }

        public DateTime StartDateTime { get; set; }

        public string CoachId { get; set; }

        [ForeignKey("CoachId")]
        public ApplicationUser Coach { get; set; }


        public int CourseTypeId { get; set; }

        [ForeignKey("CourseTypeId")]
        public CourseType CourseType { get; set; }

        public ICollection<DogCourseSession> DogParticipants { get; set; } = new List<DogCourseSession>();
    }
}
