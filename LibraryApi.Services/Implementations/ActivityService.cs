using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Activity> _activityRepo;
        private readonly IMapper _mapper;

        public ActivityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _activityRepo = unitOfWork.GetRepository<Activity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewActivityDto>> GetActivities()
        {
            var activities = await _activityRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewActivityDto>>(activities);
        }

        public async Task<IEnumerable<ViewActivityDto>> GetUserActivities(Guid id)
        {
            var activities = await Task.FromResult(_activityRepo.GetByCondition(b => b.UserId == id, includeProperties: "Books"));
            return _mapper.Map<IEnumerable<ViewActivityDto>>(activities);
        }

        public async Task<ViewActivityDto> CreateActivity(Activity activity)
        {
            // Check if user has book already requested book by comparing the due date with new request date
            var result = await _activityRepo.AnyAsync(a => a.UserId == activity.UserId && a.BookId == activity.BookId
                        && DateTime.Compare(DateTime.Now, a.DueDate) <= 0);

            if (result)
                return null;

            var newActivity = await _activityRepo.AddAsync(activity);
            return _mapper.Map<ViewActivityDto>(newActivity);
        }

       
    }
}
