using EduCanin.Models.Entities;
using EduCanin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduCanin.Service.Interfaces
{
    public interface ICourseSessionService
    {
        Task<IEnumerable<CourseSession>> GetAllAsync();
        Task<CourseSession?> GetByIdAsync(int id);
        Task AddAsync(CourseSession courseSession);
        Task UpdateAsync(CourseSession courseSession);
        Task DeleteAsync(int id);
        Task<CourseSessionsFilterPaginationViewModel> GetSessionsForCourseTypeAsync(int courseTypeId, DateOnly? date, bool? onlyAvailable, int page, int pageSize);
    }
}
