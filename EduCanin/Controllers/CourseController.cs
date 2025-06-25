using EduCanin.Models.Entities;
using EduCanin.Models.ViewModels;
using EduCanin.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseTypeService _courseTypeService;

        public CourseController(ICourseTypeService courseTypeService)
        {
            _courseTypeService = courseTypeService;
        }

        public async Task<IActionResult> Index()
        {
            List<CourseViewModel> courseViewModels = await _courseTypeService.GetAllCoursesForDisplayAsync();
            return View(courseViewModels);
        }
    }
}
