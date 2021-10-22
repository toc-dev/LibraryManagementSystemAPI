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
            
            CreateMap<Author, ViewAuthorDto>()
                .ForMember(c => c.FullName, opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));

            CreateMap<AuthorForCreationDto, Author>();

            CreateMap<AuthorForUpdateDto, Author>();

            CreateMap<Book, ViewBookDto>()
                .ForMember(dest => dest.YearPublished,
                opt => opt.MapFrom(src => src.YearPublished.Year));

            CreateMap<BookForUpdateDto, Book>();

            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, ViewCategoryDto>().ReverseMap();
        }
    }
}
