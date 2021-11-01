using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<(IdentityResult, User)> CreateUserAsync(UserForRegistrationDTO userForRegistration);
        Task<User> PatchUserAsync(string id, JsonPatchDocument<UserForUpdateDTO> userForUpdate);
        Task<User> FindUserAsync(string id);
        Task<User> DeleteUserAsync(string id);
    }
}
