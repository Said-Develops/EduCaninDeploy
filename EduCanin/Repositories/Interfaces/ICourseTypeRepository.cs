using EduCanin.Models.Entities;

namespace EduCanin.Repositories.Interfaces
{
    public interface ICourseTypeRepository
    {
        Task<IEnumerable<CourseType>> GetAllAsync();
        Task<CourseType?> GetByIdAsync(int id);
        Task AddAsync(CourseType courseType);
        void Update(CourseType courseType);
        void Delete(CourseType courseType);
        Task SaveChangesAsync();


    }
}
