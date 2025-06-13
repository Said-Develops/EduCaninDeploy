namespace EduCanin.ViewModels
{
    public class CourseSessionsFilterPaginationViewModel
    {
        public int CourseTypeId { get; set; }
        public string CourseTypeTitle { get; set; }
        public List<CourseSessionViewModel> Sessions { get; set; } = new();

        // Filtres
        public DateOnly? SelectedDate { get; set; }      // Date sélectionnée
        public bool? OnlyAvailable { get; set; }         // Filtre "places dispo"

        // Pagination
        public int PageIndex { get; set; }               // Page actuelle
        public int TotalPages { get; set; }              // Nb total de pages
    }
}
