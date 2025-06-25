using System.ComponentModel.DataAnnotations;

namespace EduCanin.Models.Entities
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseType> CourseTypesWithRestriction { get; set; } = new List<CourseType>();
    }
}
