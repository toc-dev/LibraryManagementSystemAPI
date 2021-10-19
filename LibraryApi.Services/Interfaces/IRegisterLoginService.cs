using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IRegisterLoginService
    {
        int? Add(User user);
        public User Add(UserForRegistrationDTO registerUser);
        public User GetUser(string userId);
    }
}
