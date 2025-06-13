using System.Security.Claims;
using EduCanin.Models.Entities;
using EduCanin.Repositories;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;

namespace EduCanin.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _applicationUserRepository = applicationUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(ApplicationUser applicationUser)
        {
            await _applicationUserRepository.AddAsync(applicationUser);
            await _applicationUserRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string guid)
        {
            ApplicationUser? applicationUserToDelete = await _applicationUserRepository.GetByIdAsync(guid);
            if(applicationUserToDelete != null)
            {
                _applicationUserRepository.Delete(applicationUserToDelete);
                await _applicationUserRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _applicationUserRepository.GetAllAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string guid)
        {
            return await _applicationUserRepository.GetByIdAsync(guid);
        }

        public async Task<ApplicationUser?> GetCurrentUserWithDogsAsync()
        {
            ClaimsPrincipal? userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                return null;
            }
            string? userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return null;
            }

            return await _applicationUserRepository.GetUserWithDogsByIdAsync(userId);
        }

        public async Task UpdateAsync(ApplicationUser applicationUser)
        {
            _applicationUserRepository.Update(applicationUser);
            await _applicationUserRepository.SaveChangesAsync();
        }
    }
}
