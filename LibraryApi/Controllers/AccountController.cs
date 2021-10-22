using AutoMapper;
using LibraryApi.ActionFilters;
using LibraryApi.Data.Implementations;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authenticationManager;
        
        public AccountController(IdentityContext context,
            IMapper mapper,
            UserManager<User> userManager,
            IAuthenticationManager authenticationManager
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("users"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserForRegistrationDTO userForRegistration)
        {         
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            var userFromDb = await _userManager.FindByNameAsync(user.UserName);
            var claim = new Claim("UserType", userForRegistration.UserType);
            await _userManager.AddClaimAsync(userFromDb, claim);
            
            return Ok(user);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (!await _authenticationManager.ValidateUser(user))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await _authenticationManager.CreateToken() });
        }
    }
}
