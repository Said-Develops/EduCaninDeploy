using EduCanin.Models.Entities;
using EduCanin.Service.Interfaces;
using EduCanin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "News");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "News");
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // On appelle le service d’inscription avec les données du formulaire.
            // Cette méthode retourne un IdentityResult qui indique si la création du compte a réussi ou échoué.
            IdentityResult result = await _accountService.RegisterAsync(model);

            if (result.Succeeded)
            {
                // Si l'inscription a réussi, on redirige l'utilisateur vers la page d'accueil des cours.
                return RedirectToAction("Index", "News");
            }

            // Si on arrive ici, c’est que l’inscription a échoué.
            // Les raisons possibles : email déjà utilisé, mot de passe trop court, etc.
            // Toutes ces erreurs sont renvoyées sous forme de IdentityError dans result.Errors.

            // On parcourt chaque erreur retournée par Identity
            foreach (IdentityError error in result.Errors)
            {
                // On ajoute l’erreur au ModelState pour pouvoir les afficher dans la vue (en haut du formulaire par exemple)
                ModelState.AddModelError("", error.Description);
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Appel au service pour tenter une connexion avec les identifiants fournis
            Microsoft.AspNetCore.Identity.SignInResult result = await _accountService.LoginAsync(model);

            if (result.Succeeded)
            {
                // Redirection en cas de succès
                return RedirectToAction("Index", "News");
            }

            // Gestion des cas d'échec
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Votre compte est temporairement verrouillé.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Connexion non autorisée. Vérifiez votre email.");
            }else
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect");
            }

                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }


    }

}
