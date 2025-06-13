using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAllAsync();
        Task<Dog?> GetByIdAsync(int id);
        Task AddAsync(Dog dog);
        void Update(Dog dog);
        void Delete(Dog dog);
        Task SaveChangesAsync();
    }
}
