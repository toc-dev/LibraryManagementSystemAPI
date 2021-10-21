using LibraryApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Models.Enumerators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData
            (
                new Author
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Harry",
                    LastName = "Wills",
                    Email = "harrywills@domain.com",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1992, 11, 11),
                    CreatedBy = "Gideon",
                },

                new Author
                {
                    Id = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                    FirstName = "Terry",
                    LastName = "Graham",
                    Email = "terrygraham@domain.com",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1995, 3, 1),
                    CreatedBy = "Gideon"
                }
            );
        }
    }
}
