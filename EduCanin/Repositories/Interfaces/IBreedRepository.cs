using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface IBreedRepository
    {
        Task<IEnumerable<Breed>> GetAllAsync();
        Task<Breed?> GetByIdAsync(int id);
        Task AddAsync(Breed Breed);
        void Update(Breed Breed);
        void Delete(Breed Breed);
        Task SaveChangesAsync();
    }
}
