using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;

namespace EduCanin.Service
{
    public class BreedService : IBreedService
    {
        private readonly IBreedRepository _breedRepository;

        public BreedService(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task AddAsync(Breed Breed)
        {
            await _breedRepository.AddAsync(Breed);
            await _breedRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Breed? Breed = await _breedRepository.GetByIdAsync(id);
            if (Breed != null)
            {
                _breedRepository.Delete(Breed);
                await _breedRepository.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            return await _breedRepository.GetAllAsync();
        }

        public async Task<Breed?> GetByIdAsync(int id)
        {
            return await _breedRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Breed Breed)
        {
            _breedRepository.Update(Breed);
            await _breedRepository.SaveChangesAsync();
        }
    }
}
