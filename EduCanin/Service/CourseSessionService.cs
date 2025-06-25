using EduCanin.Models.Entities;
using EduCanin.Models.ViewModels;
using EduCanin.Repositories.Interfaces;
using EduCanin.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduCanin.Service
{
    public class CourseSessionService : ICourseSessionService
    {
        private readonly ICourseSessionRepository _courseSessionRepository;
        private readonly ICourseTypeService _courseTypeService;
        private readonly IApplicationUserService _applicationUserService;

        public CourseSessionService(ICourseSessionRepository courseSessionRepository, ICourseTypeService courseTypeService, IApplicationUserService applicationUserService)
        {
            _courseSessionRepository = courseSessionRepository;
            _courseTypeService = courseTypeService;
            _applicationUserService = applicationUserService;
        }

        public async Task AddAsync(CourseSession courseSession)
        {
            await _courseSessionRepository.AddAsync(courseSession);
            await _courseSessionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            CourseSession? courseSession = await _courseSessionRepository.GetByIdAsync(id);
            if (courseSession != null)
            {
                _courseSessionRepository.Delete(courseSession);
                await _courseSessionRepository.SaveChangesAsync();
            }
                
        }

        public async Task<IEnumerable<CourseSession>> GetAllAsync()
        {
            return await _courseSessionRepository.GetAllAsync();
        }

        public async Task<CourseSession?> GetByIdAsync(int id)
        {
            return await _courseSessionRepository.GetByIdAsync(id);
        }

        public async Task<CourseSession?> GetByIdAsyncWithAll(int id)
        {
            return await _courseSessionRepository.GetByIdAsyncWithAll(id);
        }

        public async Task UpdateAsync(CourseSession courseSession)
        {
            _courseSessionRepository.Update(courseSession);
            await _courseSessionRepository.SaveChangesAsync();
        }

        public async Task<CourseSessionsFilterPaginationViewModel> GetSessionsForCourseTypeAsync(int courseTypeId,DateOnly? date,bool? onlyAvailable,int page,int pageSize)
        {
            CourseType? courseType = await _courseTypeService.GetByIdAsync(courseTypeId);

            if (courseType == null)
            {
                return null;
            }

            IEnumerable<CourseSession> sessions = courseType.Sessions;

            if (date.HasValue)
            {
                //Ici on met les deux valeur en datetime et comme horaire 00:00 pour pouvoir les comparer
                DateTime targetDate = date.Value.ToDateTime(TimeOnly.MinValue).Date;
                sessions = sessions.Where(s => s.StartDateTime.Date == targetDate);
            }

            if (onlyAvailable == true)
            {
                sessions = sessions.Where(s => s.DogParticipants.Count < courseType.ParticipantsMaximal);
            }
            

            int totalSessions = sessions.Count();

            IEnumerable<CourseSession> pagedSessions = sessions
                .Where(s => s.StartDateTime >= DateTime.Today)
                .OrderBy(s => s.StartDateTime)
                //Skip nous sert a pas prendre les X premiers resultats en fonction de la page ou on se trouve 
                //par exemple si on est a la page 2 donc .Skip(2-1)* 9) on aura pas les 9 premiers
                .Skip((page - 1) * pageSize)
                //Le take il sert a prendre dans la liste que le nombre de sessions demandé ici c'est 9 
                //et il laisse tomber tous les autres
                .Take(pageSize)
                .ToList();

            List<CourseSessionViewModel> sessionViewModels = new List<CourseSessionViewModel>();
            foreach (CourseSession session in pagedSessions)
            {
                CourseSessionViewModel sessionViewModel = new CourseSessionViewModel
                {
                    Id = session.Id,
                    SessionDate = session.StartDateTime,
                    //ICI IL FAUT voir si j'ai le include dans sessions
                    CoachName = session.Coach != null ? session.Coach.FirstName : string.Empty, 
                    SpotsLeft = courseType.ParticipantsMaximal - session.DogParticipants.Count,
                    IsFull = session.DogParticipants.Count >= courseType.ParticipantsMaximal,
                    StartTime = session.StartDateTime.ToString("HH:mm"),
                    EndTime = session.StartDateTime.AddMinutes(session.DurationInMinutes).ToString("HH:mm")
                };
                sessionViewModels.Add(sessionViewModel);
            }

            CourseSessionsFilterPaginationViewModel viewModel = new CourseSessionsFilterPaginationViewModel
            {
                CourseTypeId = courseType.Id,
                CourseTypeTitle = courseType.Title,
                Sessions = sessionViewModels,
                SelectedDate = date,
                OnlyAvailable = onlyAvailable,
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(totalSessions / (double)pageSize)
            };

            return viewModel;

        }

        public async Task<DogCourseSessionRegistrationViewModel?> GetInformationForRegister(int courseSessionId, string userId)
        {
            CourseSession? courseSession = await _courseSessionRepository.GetByIdAsyncWithAll(courseSessionId);
            if (courseSession == null)
            {
                return null;
            }
            CourseType courseType = courseSession.CourseType;
            ApplicationUser coach = courseSession.Coach;

            ApplicationUser? actualUser = await _applicationUserService.GetCurrentUserWithDogsAsync();



            DogCourseSessionRegistrationViewModel dogCourseSessionRegistrationViewModel = new DogCourseSessionRegistrationViewModel
            {
                CourseSessionId = courseSession.Id,
                CourseTitle = courseType.Title,
                StartDateTime = courseSession.StartDateTime,
                DurationInMinutes = courseSession.DurationInMinutes,
                CoachName = coach.FirstName,
                RegisteredDogsCount = courseSession.DogParticipants.Count,

                AgeMinimal = courseType.AgeMinimal,
                AgeMaximal = courseType.AgeMaximal,
                ParticipantsMaximal = courseType.ParticipantsMaximal,
                ForbiddenBreedNames = courseType.ForbidenBreed.Select(breed => breed.Name).ToList(),
                // ici les chiens de l'utilisateur 
                Dogs = actualUser.Dogs.Select(dog => new SelectListItem
                {
                    Value = dog.Id.ToString(),
                    Text = dog.Name
                }).ToList()
            };

            return dogCourseSessionRegistrationViewModel;
        }



    }
}
