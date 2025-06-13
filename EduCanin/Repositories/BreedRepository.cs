using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BreedRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Breed Breed)
        {
            await _applicationDbContext.Breeds.AddAsync(Breed);
        }

        public void Delete(Breed Breed)
        {
            _applicationDbContext.Breeds.Remove(Breed);
        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            return await _applicationDbContext.Breeds.ToListAsync();
        }

        public async Task<Breed?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Breeds.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(Breed Breed)
        {
            _applicationDbContext.Breeds.Update(Breed);
        }
    }
}
