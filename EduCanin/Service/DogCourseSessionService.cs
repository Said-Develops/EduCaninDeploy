using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;

namespace EduCanin.Service
{
    public class DogCourseSessionService : IDogCourseSessionService
    {
        private readonly IDogCourseSessionRepository _dogCourseSessionRepository;

        public DogCourseSessionService(IDogCourseSessionRepository dogCourseSessionRepository)
        {
            _dogCourseSessionRepository = dogCourseSessionRepository;
        }

        public async Task AddAsync(DogCourseSession dogCourseSession)
        {
            await _dogCourseSessionRepository.AddAsync(dogCourseSession);
            await _dogCourseSessionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            DogCourseSession? dogCourseSession = await _dogCourseSessionRepository.GetByIdAsync(id);
            if(dogCourseSession != null)
            {
                _dogCourseSessionRepository.Delete(dogCourseSession);
                await _dogCourseSessionRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DogCourseSession>> GetAllAsync()
        {
            return await _dogCourseSessionRepository.GetAllAsync();
        }

        public async Task<DogCourseSession?> GetByIdAsync(int id)
        {
            return await _dogCourseSessionRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(DogCourseSession dogCourseSession)
        {
            _dogCourseSessionRepository.Update(dogCourseSession);
            await _dogCourseSessionRepository.SaveChangesAsync();
        }
    }
}
