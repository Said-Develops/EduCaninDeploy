using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCanin.Models.Entities
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly BirthDate { get; set; }

        public DogGender DogGender { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public string ApplicationUserId { get; set; }

        public int BreedId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("BreedId")]
        public Breed Breed { get; set; }

        public ICollection<DogCourseSession> DogCourseSessions { get; set; } = new List<DogCourseSession>();


    }
}
