using EduCanin.Models.Entities;
using System.Diagnostics.Metrics;
using EduCanin.Service.Interfaces;
using EduCanin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async  Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

            return result;
        }


        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {

            ApplicationUser newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };


            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
                return result;

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(newUser, "User");
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return IdentityResult.Success;

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }

}
