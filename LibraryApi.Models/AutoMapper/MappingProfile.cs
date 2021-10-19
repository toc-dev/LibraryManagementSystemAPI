using AutoMapper;
using LibraryApi.Models.Entities;
using LibraryApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDTO, User>();
                //.ForMember(dest =>
                //dest.FirstName,
                //opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.LastName,
                //opt => opt.MapFrom(src => src.LastName))
                //.ForMember(dest => dest.Email,
                //opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.Password,
                //opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserForRegistrationDTO>();
        }
    }
}
