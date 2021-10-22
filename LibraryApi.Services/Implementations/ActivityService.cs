using LibraryApi.Data.Interfaces;
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
        private readonly IRepository<User> _userRepo;

        public ActivityService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _activityRepo = unitOfWork.GetRepository<Activity>();
            _userRepo = unitOfWork.GetRepository<User>();

        }
        public async Task<IEnumerable<Activity>> GetActivities()
        {
            return await _activityRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Activity>> GetUserActivities(Guid id)
        {
            return await Task.FromResult(_activityRepo.GetByCondition(b => b.UserId == id));
        }

        public async Task<Activity> CreateActivity(Activity activity)
        {
            return await _activityRepo.AddAsync(activity);
        }
    }
}
