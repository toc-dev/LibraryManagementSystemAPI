using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ViewActivityDto>> GetActivities();
        Task<IEnumerable<ViewActivityDto>> GetUserActivities(Guid id);
        Task<Activity> CreateActivity(Activity activity);
    }
}
