using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(string guid);
        Task AddAsync(ApplicationUser applicationUser);
        void Update(ApplicationUser applicationUser);
        void Delete(ApplicationUser applicationUser);
        Task SaveChangesAsync();
        Task<ApplicationUser?> GetUserWithDogsByIdAsync(string guid);

        Task<ApplicationUser?> GetUserWithFullSessionDataByIdAsync(string userId);


    }
}
