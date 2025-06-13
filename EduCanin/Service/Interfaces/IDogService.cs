using System.Security.Claims;
using EduCanin.Models.Entities;

namespace EduCanin.Service.Interfaces
{
    public interface IDogService
    {
        Task<IEnumerable<Dog>> GetAllAsync();
        Task<Dog?> GetByIdAsync(int id);
        Task AddAsync(Dog dog);
        Task UpdateAsync(Dog dog);
        Task DeleteAsync(int id);


    }
}
