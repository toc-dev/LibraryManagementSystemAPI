using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implementations
{
    public class RegisterLoginService : IRegisterLoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        public RegisterLoginService(IUnitOfWork unitOfWork, IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public int? Add(User user)
        {
            _userRepository.Add(user);
            int? result = _unitOfWork.SaveChanges();
            return result;
        }

        public User Add(UserForRegistrationDTO registerUser)
        {
            int affectedRows = default;
            var user = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                //DateOfBirth = registerUser.DateOfBirth,
                //Gender = registerUser.Gender,
                Password = registerUser.Password
            };
            _userRepository.Add(user);
            affectedRows = _unitOfWork.SaveChanges();
            return user;
        }

        public User GetUser(string userId)
        {
            return _userRepository.GetById(userId);
        }
    }
}
