using System.Threading.Tasks;
using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class CourseSessionRepository : ICourseSessionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseSessionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(CourseSession courseSession)
        {
            await _applicationDbContext.CourseSessions.AddAsync(courseSession);
        }
         
        public void Delete(CourseSession courseSession)
        {
            _applicationDbContext.CourseSessions.Remove(courseSession);
        }

        public async Task<IEnumerable<CourseSession>> GetAllAsync()
        {
            return await _applicationDbContext.CourseSessions.ToListAsync();
        }

        public async Task<CourseSession?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.CourseSessions.FindAsync(id);
        }

        public async Task<CourseSession?> GetByIdAsyncWithAll(int id)
        {
            return await _applicationDbContext.CourseSessions
                .Include(cs => cs.CourseType)
                    .ThenInclude(ct => ct.ForbidenBreed)
                .Include(cs => cs.DogParticipants)
                    .ThenInclude(dp => dp.Dog)
                .Include(cs => cs.Coach)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(CourseSession courseSession)
        {
            _applicationDbContext.CourseSessions.Update(courseSession);
        }
    }
}
