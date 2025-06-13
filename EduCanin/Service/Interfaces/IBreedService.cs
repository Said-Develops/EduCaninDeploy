using EduCanin.Models.Entities;

namespace EduCanin.Service.Interfaces
{
    public interface IBreedService
    {
        Task<IEnumerable<Breed>> GetAllAsync();
        Task<Breed?> GetByIdAsync(int id);
        Task AddAsync(Breed breed);
        Task UpdateAsync(Breed breed);
        Task DeleteAsync(int id);
    }
}
