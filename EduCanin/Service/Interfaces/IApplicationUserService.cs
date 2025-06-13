using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;

namespace EduCanin.Service.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string guid);
        Task AddAsync(ApplicationUser applicationUser);
        Task UpdateAsync(ApplicationUser applicationUser);
        Task DeleteAsync(string guid);
        Task<ApplicationUser?> GetCurrentUserWithDogsAsync();

    }
}
