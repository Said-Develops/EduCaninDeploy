using EduCanin.Models.Entities;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;
using EduCanin.ViewModels;

namespace EduCanin.Service
{
    public class CourseTypeService : ICourseTypeService
    {
        private readonly ICourseTypeRepository _courseTypeRepository;

        public CourseTypeService(ICourseTypeRepository courseTypeRepository)
        {
            _courseTypeRepository = courseTypeRepository;
        }

        public async Task AddAsync(CourseType courseType)
        {
            await _courseTypeRepository.AddAsync(courseType);
            await _courseTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            CourseType? courseType = await _courseTypeRepository.GetByIdAsync(id);
            if(courseType != null)
            {
                _courseTypeRepository.Delete(courseType);
                await _courseTypeRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CourseType>> GetAllAsync()
        {
            return await _courseTypeRepository.GetAllAsync();
        }

        public async Task<CourseType?> GetByIdAsync(int id)
        {
            return await _courseTypeRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(CourseType courseType)
        {
            _courseTypeRepository.Update(courseType);
            await _courseTypeRepository.SaveChangesAsync();
        }

        public async Task<List<CourseViewModel>> GetAllCoursesForDisplayAsync()
        {
            List<CourseType> courses = (await _courseTypeRepository.GetAllAsync()).ToList();
            List<CourseViewModel> viewModelCourses = new List<CourseViewModel>();

            foreach (CourseType course in courses)
            {
                CourseViewModel courseViewModel = new CourseViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description
                };
                viewModelCourses.Add(courseViewModel);
            }

            return viewModelCourses;
        }



    }
}
