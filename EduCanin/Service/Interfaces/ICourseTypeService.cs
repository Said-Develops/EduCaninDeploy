using EduCanin.Models.Entities;
using EduCanin.ViewModels;

namespace EduCanin.Service.Interfaces
{
    public interface ICourseTypeService
    {
        Task<IEnumerable<CourseType>> GetAllAsync();
        Task<CourseType?> GetByIdAsync(int id);
        Task UpdateAsync(CourseType courseType);
        Task AddAsync(CourseType courseType);
        Task DeleteAsync(int id);
        Task<List<CourseViewModel>> GetAllCoursesForDisplayAsync();
    }
}
