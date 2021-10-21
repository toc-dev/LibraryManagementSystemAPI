using AutoMapper;
using LibraryApi.Models.Entities;
using LibraryApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.MappingConfiguration
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDTO, User>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<User, UserForRegistrationDTO>();
        }
    }
}
