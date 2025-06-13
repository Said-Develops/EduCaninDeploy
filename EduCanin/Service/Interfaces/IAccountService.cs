using EduCanin.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EduCanin.Service.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        Task LogoutAsync();
    }
}
