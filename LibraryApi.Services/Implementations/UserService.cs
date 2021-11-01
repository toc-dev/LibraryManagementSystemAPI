using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;

using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace LibraryApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<(IdentityResult, User)> CreateUserAsync([FromBody] UserForRegistrationDTO userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            return (result, user);

        }

        public async Task<User> PatchUser(string id, [FromBody] JsonPatchDocument<UserForUpdateDTO> userForUpdate)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return user;
            }
            var userToUpdate = _mapper.Map<UserForUpdateDTO>(user);
            userForUpdate.ApplyTo(userToUpdate);
            _mapper.Map(userToUpdate, user);

            return (user);
        }
    }
}

//I don't know why, but it appears injecting a IRepository<User> into a system containing IUserManager will cause the system to crash and burn
