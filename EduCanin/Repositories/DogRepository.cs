using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DogRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Dog dog)
        {
            await _applicationDbContext.Dogs.AddAsync(dog);
        }

        public void Delete(Dog dog)
        {
            _applicationDbContext.Dogs.Remove(dog);
        }

        public async Task<IEnumerable<Dog>> GetAllAsync()
        {
            return await _applicationDbContext.Dogs.ToListAsync();
        }

        public async Task<Dog?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Dogs
                .Include(dog => dog.Breed)
                .Include(dog => dog.DogCourseSessions)
                    .ThenInclude(DogCourseSession => DogCourseSession.CourseSession)
                .FirstAsync(dog => dog.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(Dog dog)
        {
            _applicationDbContext.Update(dog);
        }
    }
}
