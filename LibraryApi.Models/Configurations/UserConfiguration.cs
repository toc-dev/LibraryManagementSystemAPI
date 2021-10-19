using LibraryApi.Models.Entities;
using LibraryApi.Models.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Configurations
{
    class UserConfiguration : AuthorConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
                (
                new User
                {
                    Id = new string("c9d4c053-49b6-410c-bc78-2d54a9991881"),
                    FirstName = "Jeremy",
                    LastName = "Doe",
                    Email = "jeremy.doe@domain.com",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1991, 11, 11),
                },
            new User
            {
                Id = new string("c9d4c053-49b6-410c-bc78-2d54a99918882"),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@domain.com",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1991, 11, 11),
            },
            new User
            {
                Id = new string("c9d4c053-49b6-410c-bc78-2d54a99918883"),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@domain.com",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1990, 10, 11),
            });
        }
    }
}
