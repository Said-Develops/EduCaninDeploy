using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface IDogCourseSessionRepository
    {
        Task<IEnumerable<DogCourseSession>> GetAllAsync();
        Task<DogCourseSession?> GetByIdAsync(int id);
        Task AddAsync(DogCourseSession dogCourseSession);
        void Update(DogCourseSession dogCourseSession);
        void Delete(DogCourseSession dogCourseSession);
        Task SaveChangesAsync();
    }
}
