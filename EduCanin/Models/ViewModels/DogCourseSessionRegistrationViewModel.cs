using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduCanin.Models.ViewModels
{
    public class DogCourseSessionRegistrationViewModel
    {
        // Session info
        public int CourseSessionId { get; set; }
        public string CourseTitle { get; set; }
        public DateTime StartDateTime { get; set; }
        public string StartTime => StartDateTime.ToString("HH:mm");
        public string EndTime => StartDateTime.AddMinutes(DurationInMinutes).ToString("HH:mm");
        public string CoachName { get; set; }
        public int DurationInMinutes { get; set; }
        public int SpotsLeft => ParticipantsMaximal - RegisteredDogsCount;
        public int RegisteredDogsCount { get; set; }

        // Prérequis depuis CourseType
        public int AgeMinimal { get; set; }
        public int AgeMaximal { get; set; }
        public int ParticipantsMaximal { get; set; }
        public List<string> ForbiddenBreedNames { get; set; } = new();

        // Chiens de l’utilisateur
        public List<SelectListItem> Dogs { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner un chien.")]
        public int SelectedDogId { get; set; }
    }
}
