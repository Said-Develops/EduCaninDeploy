using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface ICourseSessionRepository
    {
        Task<IEnumerable<CourseSession>> GetAllAsync();
        Task<CourseSession?> GetByIdAsync(int id);
        Task AddAsync(CourseSession courseSession);
        void Update(CourseSession courseSession);
        void Delete(CourseSession courseSession);
        Task SaveChangesAsync();

    }
}
