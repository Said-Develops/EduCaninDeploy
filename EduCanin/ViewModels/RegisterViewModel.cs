using System.ComponentModel.DataAnnotations;

namespace EduCanin.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le prénom est requis.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmation est requise.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; }

        public bool? AcceptTerms { get; set; }

        public DateOnly RegisterDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    }
}
