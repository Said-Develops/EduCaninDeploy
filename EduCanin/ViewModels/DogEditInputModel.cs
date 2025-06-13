using System.ComponentModel.DataAnnotations;

namespace EduCanin.ViewModels
{
    public class DogEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom du chien")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Required]
        [Display(Name = "Sexe")]
        public DogGender DogGender { get; set; }

        [Required]
        [Display(Name = "Race")]
        public int? BreedId { get; set; }

        [Required]
        [Display(Name = "Poids (kg)")]
        [Range(0, 200)]
        public int? Weight { get; set; }

        [Required]
        [Display(Name = "Taille (cm)")]
        [Range(0, 200)]
        public int? Height { get; set; }
    }
}
