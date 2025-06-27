using System.Security.Claims;
using EduCanin.Models.Entities;
using EduCanin.Models.ViewModels;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    [Authorize]
    public class CourseSessionController : Controller
    {
        private readonly ICourseSessionService _courseSessionService;
        private readonly ICourseTypeService _courseTypeService;
        private readonly IDogCourseSessionService _dogCourseSessionService;

        public CourseSessionController(ICourseSessionService courseSessionService, ICourseTypeService courseTypeService, IDogCourseSessionService dogCourseSessionService)
        {
            _courseSessionService = courseSessionService;
            _courseTypeService = courseTypeService;
            _dogCourseSessionService = dogCourseSessionService;
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

        public async Task<IActionResult> CourseSessionRegister(int courseSessionId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DogCourseSessionRegistrationViewModel? dogCourseSessionRegistrationViewModel = await _courseSessionService.GetInformationForRegister(courseSessionId, userId);
            if (dogCourseSessionRegistrationViewModel == null)
            {
                return NotFound();
            }

            return View(dogCourseSessionRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseSessionRegister(DogCourseSessionRegistrationViewModel dogCourseSessionRegistrationViewModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                DogCourseSessionRegistrationViewModel? dogCourseSessionRegistrationViewModelRefresh = await _courseSessionService.GetInformationForRegister(dogCourseSessionRegistrationViewModel.CourseSessionId, userId);
                return View(dogCourseSessionRegistrationViewModelRefresh);

            }

            string statusMessage = await _courseSessionService.CheckIfDogCanBeRegisteredAsync(dogCourseSessionRegistrationViewModel.SelectedDogId, dogCourseSessionRegistrationViewModel.CourseSessionId);
            TempData["SuccessMessage"] = statusMessage;

            if (statusMessage != "Inscription validée !")
            {
                ModelState.AddModelError("",statusMessage);
                DogCourseSessionRegistrationViewModel? refreshedViewModel = await _courseSessionService.GetInformationForRegister(dogCourseSessionRegistrationViewModel.CourseSessionId, userId);
                return View(refreshedViewModel);
            }

            DogCourseSession registration = new DogCourseSession
            {
                DogId = dogCourseSessionRegistrationViewModel.SelectedDogId,
                CourseSessionId = dogCourseSessionRegistrationViewModel.CourseSessionId,
                RegistrationDate = DateTime.UtcNow,
                WasPresent = false
            };

            await _dogCourseSessionService.AddAsync(registration);


            return RedirectToAction("Index","Course");
        }

    }
}
