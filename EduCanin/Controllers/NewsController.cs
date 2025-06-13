using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
