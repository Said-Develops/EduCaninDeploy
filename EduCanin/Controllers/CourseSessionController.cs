using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;
using EduCanin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    public class CourseSessionController : Controller
    {
        private readonly ICourseSessionService _courseSessionService;
        private readonly ICourseTypeService _courseTypeService;

        public CourseSessionController(ICourseSessionService courseSessionService, ICourseTypeService courseTypeService)
        {
            _courseSessionService = courseSessionService;
            _courseTypeService = courseTypeService;
        }

        public async Task<IActionResult> CourseSessionByCourseType(int courseTypeId, DateOnly? date = null, bool? onlyAvailable = null, int page = 1, int pageSize =9)
        {
            CourseSessionsFilterPaginationViewModel? viewModel = await _courseSessionService.GetSessionsForCourseTypeAsync(courseTypeId, date, onlyAvailable, page, pageSize);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


    }
}
