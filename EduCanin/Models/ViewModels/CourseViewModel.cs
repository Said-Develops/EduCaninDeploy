namespace EduCanin.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string IconTailwindClass
        {
            get
            {
                switch (Title)
                {
                    case "École du chiot":
                        return "fas fa-baby text-indigo-500 text-xl";
                    case "Éducation 6-12 mois":
                        return "fas fa-bone text-blue-500 text-xl";
                    case "Éducation 1-2 ans":
                        return "fas fa-running text-green-500 text-xl";
                    case "Éducation +2 ans":
                        return "fas fa-award text-purple-500 text-xl";
                    case "Chiot spécial famille":
                        return "fas fa-baby-carriage text-pink-500 text-xl";
                    case "Cours avancé":
                        return "fas fa-medal text-yellow-500 text-xl";
                    case "Défense & protection (Chiens d’attaque)":
                        return "fas fa-shield-halved text-red-500 text-xl";
                    case "Instinct de troupeau (Chiens de berger)":
                        return "fas fa-people-group text-lime-500 text-xl";
                    case "Réactivité & canalisation (Chiens primitifs)":
                        return "fas fa-compass text-gray-700 text-xl";
                    default:
                        return "fas fa-dog text-gray-500 text-xl";
                }
            }
        }

        public string BoxIconTailwindClass
        {
            get
            {
                switch (Title)
                {
                    case "École du chiot":
                        return "bg-indigo-100 p-3 rounded-full mr-4";
                    case "Éducation 6-12 mois":
                        return "bg-blue-100 p-3 rounded-full mr-4";
                    case "Éducation 1-2 ans":
                        return "bg-green-100 p-3 rounded-full mr-4";
                    case "Éducation +2 ans":
                        return "bg-purple-100 p-3 rounded-full mr-4";
                    case "Chiot spécial famille":
                        return "bg-pink-100 p-3 rounded-full mr-4";
                    case "Cours avancé":
                        return "bg-yellow-100 p-3 rounded-full mr-4";
                    case "Défense & protection (Chiens d’attaque)":
                        return "bg-red-100 p-3 rounded-full mr-4";
                    case "Instinct de troupeau (Chiens de berger)":
                        return "bg-lime-100 p-3 rounded-full mr-4";
                    case "Réactivité & canalisation (Chiens primitifs)":
                        return "bg-gray-200 p-3 rounded-full mr-4";
                    default:
                        return "bg-gray-100 p-3 rounded-full mr-4";
                }
            }
        }

        public string ButtonTailwindClass
        {
            get
            {
                switch (Title)
                {
                    case "École du chiot":
                        return "view-sessions-btn w-full py-2 bg-indigo-500 text-white rounded-md hover:bg-indigo-600 transition font-medium";
                    case "Éducation 6-12 mois":
                        return "view-sessions-btn w-full py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition font-medium";
                    case "Éducation 1-2 ans":
                        return "view-sessions-btn w-full py-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition font-medium";
                    case "Éducation +2 ans":
                        return "view-sessions-btn w-full py-2 bg-purple-500 text-white rounded-md hover:bg-purple-600 transition font-medium";
                    case "Chiot spécial famille":
                        return "view-sessions-btn w-full py-2 bg-pink-500 text-white rounded-md hover:bg-pink-600 transition font-medium";
                    case "Cours avancé":
                        return "view-sessions-btn w-full py-2 bg-yellow-500 text-white rounded-md hover:bg-yellow-600 transition font-medium";
                    case "Défense & protection (Chiens d’attaque)":
                        return "view-sessions-btn w-full py-2 bg-red-500 text-white rounded-md hover:bg-red-600 transition font-medium";
                    case "Instinct de troupeau (Chiens de berger)":
                        return "view-sessions-btn w-full py-2 bg-lime-500 text-white rounded-md hover:bg-lime-600 transition font-medium";
                    case "Réactivité & canalisation (Chiens primitifs)":
                        return "view-sessions-btn w-full py-2 bg-gray-700 text-white rounded-md hover:bg-gray-800 transition font-medium";
                    default:
                        return "view-sessions-btn w-full py-2 bg-gray-500 text-white rounded-md hover:bg-gray-600 transition font-medium";
                }
            }
        }

        public string DataCategoryFilter
        {
            get
            {
                switch (Title)
                {
                    case "École du chiot":
                    case "Chiot spécial famille":
                        return "puppy"; // 2-6 mois
                    case "Éducation 6-12 mois":
                    case "Instinct de troupeau (Chiens de berger)":
                    case "Réactivité & canalisation (Chiens primitifs)":
                        return "junior"; // dès 6 mois
                    case "Éducation 1-2 ans":
                    case "Défense & protection (Chiens d’attaque)":
                        return "adult"; // dès 12 mois
                    case "Éducation +2 ans":
                    case "Cours avancé":
                        return "senior"; // dès 24-25 mois
                    default:
                        return "";
                }
            }
        }
    }
}
