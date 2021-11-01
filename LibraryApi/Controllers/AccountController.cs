﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUserService _userService;
        

        public AccountController(IdentityContext context,
            IMapper mapper,
            UserManager<User> userManager,
            IUserService userService,
            IAuthenticationManager authenticationManager)
            
        {
            _context = context;
            _mapper = mapper;
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
            await _userManager.AddToRoleAsync(result.Item2, userForRegistration.Role.ToString());

            return Ok(result.Item2);
        }


        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (user == null) return BadRequest("Invalid!");
            if (!await _authenticationManager.ValidateUser(user))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await _authenticationManager.CreateToken() });
        }


        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(user);
        }


        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] JsonPatchDocument<UserForUpdateDTO> userForUpdate)
        {
            if (userForUpdate == null) return BadRequest("Object is null");
            var user = await _userService.PatchUser(id, userForUpdate);
            if (user == null) return BadRequest("User not found!");
            await _userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return BadRequest("User not found!");
            var rolesForUser = await _userManager.GetRolesAsync(user);
            await _userManager.DeleteAsync(user);
            return Ok(user);
        }
    }
}
