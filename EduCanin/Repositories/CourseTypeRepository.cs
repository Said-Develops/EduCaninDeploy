using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class CourseTypeRepository : ICourseTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(CourseType courseType)
        {
            await _applicationDbContext.CourseTypes.AddAsync(courseType);
        }

        public void Delete(CourseType courseType)
        {
            _applicationDbContext.CourseTypes.Remove(courseType);
        }

        public async Task<IEnumerable<CourseType>> GetAllAsync()
        {
            return await _applicationDbContext.CourseTypes.ToListAsync();
        }

        public async Task<CourseType?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.CourseTypes.Include(CourseType => CourseType.Sessions)
                .ThenInclude(Sessions => Sessions.Coach)
                .FirstOrDefaultAsync(CourseType => CourseType.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(CourseType courseType)
        {
            _applicationDbContext.CourseTypes.Update(courseType);
        }

        
    }
}
