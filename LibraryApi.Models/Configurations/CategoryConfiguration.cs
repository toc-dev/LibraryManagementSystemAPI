using LibraryApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = new Guid("8bde71e7-b24a-42cd-ad44-08d98a13616c"),
                    Name = "Motivation",
                    CreatedBy = "Gideon"
                },

                new Category
                {
                    Id = new Guid("44935b91-3283-4f39-f08d-08d98a1078df"),
                    Name = "Non-fiction",
                    CreatedBy = "Gideon"
                },

                new Category
                {
                    Id = new Guid("dff0527f-8f24-4402-215f-08d98a13c3e8"),
                    Name = "Career",
                    CreatedBy = "Gideon"
                },

                new Category
                {
                    Id = new Guid("6007d5fa-3a6e-436d-8854-08d98a27ad68"),
                    Name = "Sales",
                    CreatedBy = "Gideon"
                });
        }
    }
}
