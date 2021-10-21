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
                //.ForMember(dest =>
                //dest.FirstName,
                //opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.LastName,
                //opt => opt.MapFrom(src => src.LastName))
                //.ForMember(dest => dest.Email,
                //opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.Password,
                //opt => opt.MapFrom(src => src.Password));
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<User, UserForRegistrationDTO>();
            CreateMap<Author, ViewAuthorDto>()
                .ForMember(c => c.FullName, opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));
            CreateMap<AuthorForCreationDto, Author>();
                
        }
    }
}
