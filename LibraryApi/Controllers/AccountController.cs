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
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authenticationManager;
        //private readonly IRegisterLoginService _userService;
        public AccountController(IdentityContext context,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationManager authenticationManager
            )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("users"), Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserForRegistrationDTO userForRegistration)
        {         
            var user = _mapper.Map<User>(userForRegistration);
            // check if user exists
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            var claim = new Claim("UserType", userForRegistration.UserType);
            await _userManager.AddClaimAsync(user, claim);
            
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
