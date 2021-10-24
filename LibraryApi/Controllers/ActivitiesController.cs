using LibraryApi.Extensions;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("activities")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetActivities()
        {
            return Ok(await _activityService.GetActivities());
        }

        [HttpGet("books/activities")]
        [Authorize(Policy = "RequireUserOrAuthorRole")]
        public async Task<IActionResult> GetUserActivities()
        {
            var userId = HttpContext.User.GetLoggedInUserId();
            return Ok(await _activityService.GetUserActivities(userId));
        }
        
        [HttpGet("/activities/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUserActivities(Guid id)
        {
            return Ok(await _activityService.GetUserActivities(id));
        }
    }
}
