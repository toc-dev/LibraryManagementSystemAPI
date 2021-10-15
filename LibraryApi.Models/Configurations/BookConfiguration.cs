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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData
            (
                new Book
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Title = "Success Comes At A Price",
                    ISBN = "56422299875",
                    YearPublished = new DateTime(2006, 12, 23),
                    AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                },

                new Book
                {
                    Id = new Guid("02068a01-69f5-4b6d-f678-08d98a148f09"),
                    Title = "Toolkit for Mentorship",
                    ISBN = "234977423470",
                    YearPublished = new DateTime(2018, 2, 18),
                    AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                },

                new Book
                {
                    Id = new Guid("63ab3e7c-c817-4fdb-85f8-08d98a1767fe"),
                    Title = "Principles of Expansion",
                    ISBN = "882457625741",
                    YearPublished = new DateTime(2008, 8, 13),
                    AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                },

                new Book
                {
                    Id = new Guid("e87d9b47-fc47-4e89-4ab3-08d98a1e48fe"),
                    Title = "Keys of Networking",
                    ISBN = "536648497957",
                    YearPublished = new DateTime(2017, 9, 15),
                    AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                }
            );
        }

        
    }
}
