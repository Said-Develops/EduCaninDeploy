using System.Security.Claims;
using EduCanin.Models.Entities;
using EduCanin.Models.ViewModels;
using EduCanin.Service;
using EduCanin.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IDogService _dogService;

        public DashBoardController(IApplicationUserService applicationUserService, IDogService dogService)
        {
            _applicationUserService = applicationUserService;
            _dogService = dogService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("DashBoardAdmin");

            }
            else if (User.IsInRole("Coach"))
            {

                return RedirectToAction("DashBoardCoach");
            }
            else
            {
            return RedirectToAction("DashBoardUser");

            }



        }


        [Authorize(Roles ="Admin")]
        public IActionResult DashBoardAdmin()
        {
            return View();
        }

        [Authorize(Roles ="Admin,Coach")]
        public IActionResult DashBoardCoach()
        {
            return View();
        }

        public async Task<IActionResult> DashBoardUser()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ApplicationUser? user = await _applicationUserService.GetUserWithFullSessionDataByIdAsync(userId);
            return View(user); // user.Dogs est aussi inclus
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: /Dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DogCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Récupérer l'ID de l'utilisateur connecté
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Dog newDog = new Dog
            {
                Name = model.Name,
                BirthDate = model.BirthDate,
                DogGender = model.DogGender,
                BreedId = model.BreedId.Value,
                Weight = model.Weight.Value,
                Height = model.Height.Value,
                ApplicationUserId = userId
            };

            await _dogService.AddAsync(newDog);

            return RedirectToAction("Index", "Dogs");
        }

    }
}
