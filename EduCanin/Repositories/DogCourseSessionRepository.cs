using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class DogCourseSessionRepository : IDogCourseSessionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DogCourseSessionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(DogCourseSession dogCourseSession)
        {
            await _applicationDbContext.DogCourseSessions.AddAsync(dogCourseSession);
        }

        public void Delete(DogCourseSession dogCourseSession)
        {
            _applicationDbContext.DogCourseSessions.Remove(dogCourseSession);
        }

        public async Task<IEnumerable<DogCourseSession>> GetAllAsync()
        {
            return await _applicationDbContext.DogCourseSessions.ToListAsync();
        }

        public async Task<DogCourseSession?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.DogCourseSessions.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(DogCourseSession dogCourseSession)
        {
            _applicationDbContext.DogCourseSessions.Update(dogCourseSession);
        }
    }
}
