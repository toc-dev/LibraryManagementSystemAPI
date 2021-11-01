using AutoMapper;
using LibraryApi.ActionFilters;
using LibraryApi.Data.Implementations;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApi.Services.Implementations;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUserService _userService;
        

        public AccountController(IdentityContext context,
            UserManager<User> userManager,
            IUserService userService,
            IAuthenticationManager authenticationManager)
            
        {
            _context = context;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _userService = userService;
            
        }

        [HttpGet]
        [Route("users"), Authorize(Policy = "RequireAdminRole")]
        
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        // Gideon's Review
        /* I love what you did with the tuples, 
         * but remember Davidson suggested to Alex 
         * that she could parse those values to a newly created class
         * and access that class as a return type.
         * It's up to you to decide though.
         */
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistration)
        {
            if (userForRegistration == null) return BadRequest("Invalid!!");
            var result = await _userService.CreateUserAsync(userForRegistration);

            if (!result.Item1.Succeeded)
            {
                foreach (var error in result.Item1.Errors) ModelState.TryAddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            return Ok(result.Item2);
        }


        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserForAuthenticationDTO user)
        {
            if (user == null) return BadRequest("Invalid!");
            if (!await _authenticationManager.ValidateUser(user))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await _authenticationManager.CreateToken() });
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            var user = await _userService.FindUserAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] JsonPatchDocument<UserForUpdateDTO> userForUpdate)
        {
            if (userForUpdate == null) return BadRequest("Object is null");
            var user = await _userService.PatchUserAsync(id, userForUpdate);
            if (user == null) return BadRequest("User not found!");
            await _userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user == null) return BadRequest("User not found!");
            return Ok($"{user.UserName} deleted");
        }
    }
}
