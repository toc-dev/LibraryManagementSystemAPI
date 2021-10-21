using LibraryApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Data.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthentication);
        Task<string> CreateToken();
    }
}
