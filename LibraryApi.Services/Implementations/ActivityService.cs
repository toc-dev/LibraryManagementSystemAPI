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

        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _activityRepo = unitOfWork.GetRepository<Activity>();
        }

        public async Task<IEnumerable<Activity>> GetActivities()
        {
            return await _activityRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Activity>> GetUserActivities(Guid id)
        {
            return await Task.FromResult(_activityRepo.GetByCondition(b => b.UserId == id, includeProperties: "Books"));
        }

        public async Task<Activity> CreateActivity(Activity activity)
        {
            // Check if user has book already requested book by comparing the due date with new request date
            var result = await _activityRepo.AnyAsync(a => a.UserId == activity.UserId && a.BookId == activity.BookId
                        && DateTime.Compare(DateTime.Now, a.DueDate) <= 0);

            if (result)
                return null;

            return await _activityRepo.AddAsync(activity);
        }
    }
}
