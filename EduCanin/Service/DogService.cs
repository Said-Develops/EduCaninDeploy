using System.Security.Claims;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EduCanin.Service
{
    public class DogService : IDogService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDogRepository _dogRepository;

        public DogService(UserManager<ApplicationUser> userManager, IDogRepository dogRepository)
        {
            _userManager = userManager;
            _dogRepository = dogRepository;
        }

        public async Task AddAsync(Dog dog)
        {
            await _dogRepository.AddAsync(dog);
            await _dogRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Dog? dog = await _dogRepository.GetByIdAsync(id);
            if(dog != null)
            {
                _dogRepository.Delete(dog);
                await _dogRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Dog>> GetAllAsync()
        {
            return await _dogRepository.GetAllAsync();
        }

        public async Task<Dog?> GetByIdAsync(int id)
        {
            return await _dogRepository.GetByIdAsync(id);
        }




        public async Task UpdateAsync(Dog dog)
        {
            _dogRepository.Update(dog);
            await _dogRepository.SaveChangesAsync();
        }
    }
}
