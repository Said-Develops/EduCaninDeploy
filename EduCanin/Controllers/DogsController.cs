using EduCanin.Models.Entities;
using EduCanin.Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EduCanin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EduCanin.Controllers
{
    [Authorize]
    public class DogsController : Controller
    {
        private readonly IDogService _dogService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IBreedService _breedService;

        public DogsController(IDogService dogService, IApplicationUserService applicationUserService, IBreedService breedService)
        {
            _dogService = dogService;
            _applicationUserService = applicationUserService;
            _breedService = breedService;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser? user = await _applicationUserService.GetCurrentUserWithDogsAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user); // user.Dogs est aussi inclus
        }


        public async Task<IActionResult> Create()
        {
            DogCreateViewModel dogCreateViewModel = new DogCreateViewModel
            {
                Breeds = (await _breedService.GetAllAsync()).Select(breed => new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                }),
                BirthDate = DateOnly.FromDateTime(DateTime.Now)
            };
            return View(dogCreateViewModel);
        }

        // POST: /Dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DogCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<SelectListItem> breeds = (await _breedService.GetAllAsync()).Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                });

                DogCreateViewModel dogCreateViewModel = new DogCreateViewModel
                {
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    DogGender = model.DogGender,
                    BreedId = model.BreedId.Value,
                    Weight = model.Weight.Value,
                    Height = model.Height.Value,
                    Breeds = breeds
                };

                return View(dogCreateViewModel);
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

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Dog? dog = await _dogService.GetByIdAsync(id);

            if(dog == null)
            {
                TempData["Error"] = "Ce chien d'existe pas";
                return RedirectToAction("Index");
            }

            string? currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(dog.ApplicationUserId != currentUserId)
            {
                TempData["Error"] = "Vous n'avez pas le droit de supprimer ce chien";
                return RedirectToAction("Index");
            }

            await _dogService.DeleteAsync(id);
            TempData["Sucess"] = "Chien supprimé avec succès";
            return RedirectToAction("Index","DashBoard"); 
                
            
        }

        [HttpGet]
        public async Task<IActionResult> DetailsModal(int id)
        {
            Dog? dog = await _dogService.GetByIdAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            string? currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUser == null || dog.ApplicationUserId != currentUser)
            {
                return Forbid();
            }
            return PartialView("_DogDetailsPartial", dog);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Dog? dog = await _dogService.GetByIdAsync(id);

            if (dog == null)
            {
                return NotFound();
            }

            DogEditViewModel dogEditViewModel = new DogEditViewModel
            {
                Id = dog.Id,
                Name = dog.Name,
                BirthDate = dog.BirthDate,
                DogGender = dog.DogGender,
                BreedId = dog.BreedId,
                Weight = dog.Weight,
                Height = dog.Height,
                Breeds = (await _breedService.GetAllAsync()).Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                })

            };

            return View("Edit", dogEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DogEditInputModel dogInputViewModel)
        {

            if (!ModelState.IsValid)
            {
                IEnumerable<SelectListItem> breeds = (await _breedService.GetAllAsync()).Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                });

                DogCreateViewModel dogCreateViewModel = new DogCreateViewModel
                {
                    Name = dogInputViewModel.Name,
                    BirthDate = dogInputViewModel.BirthDate,
                    DogGender = dogInputViewModel.DogGender,
                    BreedId = dogInputViewModel.BreedId.Value,
                    Weight = dogInputViewModel.Weight.Value,
                    Height = dogInputViewModel.Height.Value,
                    Breeds = breeds
                };
                return View(dogCreateViewModel);
            }

            Dog? dog = await _dogService.GetByIdAsync(dogInputViewModel.Id);

            if (dog == null)
            {
                return NotFound();
            }

            dog.Name = dogInputViewModel.Name;
            dog.BirthDate = dogInputViewModel.BirthDate;
            dog.DogGender = dogInputViewModel.DogGender;
            dog.BreedId = dogInputViewModel.BreedId.Value;
            dog.Weight = dogInputViewModel.Weight.Value;
            dog.Height = dogInputViewModel.Height.Value;

            try
            {
                await _dogService.UpdateAsync(dog);
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Une erreur est survenue lors de l'enregistrement.");
                return View(dogInputViewModel);
            }
            return RedirectToAction("Index", "DashBoard");
        }
    }
}
