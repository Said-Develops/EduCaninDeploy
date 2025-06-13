using EduCanin.Models.Entities;

namespace EduCanin.Service.Interfaces
{
    public interface IDogCourseSessionService
    {
        Task<IEnumerable<DogCourseSession>> GetAllAsync();
        Task<DogCourseSession?> GetByIdAsync(int id);
        Task AddAsync(DogCourseSession dogCourseSession);
        Task UpdateAsync(DogCourseSession dogCourseSession);
        Task DeleteAsync(int id);

    }
}
