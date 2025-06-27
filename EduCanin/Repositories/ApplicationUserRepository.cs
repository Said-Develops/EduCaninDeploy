using EduCanin.Data;
using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduCanin.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApplicationUserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task AddAsync( ApplicationUser applicationUser)
        {
             await _applicationDbContext.ApplicationUsers.AddAsync(applicationUser);
        }

        public void Delete(ApplicationUser applicationUser)
        {
            _applicationDbContext.ApplicationUsers.Remove(applicationUser);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _applicationDbContext.ApplicationUsers.ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string guid)
        {
            return await _applicationDbContext.ApplicationUsers.FindAsync(guid);
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Update(ApplicationUser applicationUser)
        {
            _applicationDbContext.ApplicationUsers.Update(applicationUser);
        }

        public async Task<ApplicationUser?> GetUserWithDogsByIdAsync(string guid)
        {
            return await _applicationDbContext.ApplicationUsers
                .Include(ApplicationUser => ApplicationUser.Dogs)
                    .ThenInclude(dogs => dogs.Breed)
                .Include(ApplicationUser => ApplicationUser.Dogs)
                    .ThenInclude(dogs => dogs.DogCourseSessions)
                        .ThenInclude(DogCourseSession => DogCourseSession.CourseSession)
                .FirstOrDefaultAsync(ApplicationUser => ApplicationUser.Id == guid);
        }

        public async Task<ApplicationUser?> GetUserWithFullSessionDataByIdAsync(string userId)
        {
            return await _applicationDbContext.ApplicationUsers
                .Where(applicationUser => applicationUser.Id == userId)
                .Include(applicationUser => applicationUser.Dogs)
                    .ThenInclude(dog => dog.DogCourseSessions)
                        .ThenInclude(dogCourseSession => dogCourseSession.CourseSession)
                            .ThenInclude(courseSession => courseSession.CourseType)
                .Include(applicationUser => applicationUser.Dogs)
                    .ThenInclude(dog => dog.DogCourseSessions)
                        .ThenInclude(dogCourseSession => dogCourseSession.CourseSession)
                                .ThenInclude(courseSession => courseSession.Coach)
                .FirstOrDefaultAsync();
        }

    }
}
