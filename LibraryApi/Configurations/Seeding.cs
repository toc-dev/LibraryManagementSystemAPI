using Microsoft.AspNetCore.Identity;
using LibraryApi.Models.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApi.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public static class Seeding
    {
        private static RoleManager<Role> _roleManager;
        private static UserManager<User> _userManager;
        private const string User1 = "Gideon";
        private const string User1Password = "Gideon@12345678";
        private const string User2 = "Sage";
        private const string User2Password = "Sage@12345678";
        private const string User3 = "Michael";
        private const string User3Password = "Michael@12345678";

        public static async void SeedUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<IdentityContext>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                await context.Database.MigrateAsync();

            _roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<Role>>();

            _userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<User>>();

            await CreateRole(AppRole.Admin.ToString());
            await CreateRole(AppRole.Author.ToString());
            await CreateRole(AppRole.User.ToString());

            var user1 = await _userManager.FindByNameAsync(User1);
            if (user1 != null) return;

            user1 = new User
            {
                FirstName = "Gideon",
                LastName = "Akunana",
                Email = "gideonakunana@domain.com",
                UserName = "g.akunana",
                PhoneNumber = "+2348160451288",
                EmailConfirmed = true,
                DateOfBirth = new DateTime(1993, 2, 15),
                Gender = Gender.Male,
                CreatedBy = "Gideon",
            };


            var createUser1 = await _userManager.CreateAsync(user1, User1Password);
            if (createUser1.Succeeded) 
                await _userManager.AddToRoleAsync(user1, AppRole.Admin.ToString());


            var user2 = await _userManager.FindByNameAsync(User2);
            if (user2 != null) return;

            user2 = new User
            {
                FirstName = "Tochukwu",
                LastName = "Sage",
                Email = "tochukwusage@domain.com",
                UserName = "t.sage",
                PhoneNumber = "+2348160451288",
                EmailConfirmed = true,
                DateOfBirth = new DateTime(1993, 2, 15),
                Gender = Gender.Male,
                CreatedBy = "Gideon",
            };


            var createUser2 = await _userManager.CreateAsync(user2, User2Password);
            if (createUser2.Succeeded) 
                await _userManager.AddToRoleAsync(user2, AppRole.Author.ToString());

            var user3 = await _userManager.FindByNameAsync(User3);
            if (user3 != null) return;

            user3 = new User
            {
                FirstName = "Kenechukwu",
                LastName = "Michael",
                Email = "kcmichael@domain.com",
                UserName = "k.michael",
                PhoneNumber = "+2348160451288",
                EmailConfirmed = true,
                DateOfBirth = new DateTime(1993, 2, 15),
                Gender = Gender.Male,
                CreatedBy = "Gideon",
            };


            var createUser3 = await _userManager.CreateAsync(user3, User3Password);
            if (createUser3.Succeeded)
                await _userManager.AddToRoleAsync(user3, AppRole.User.ToString());
        }

        public static async void SeedAuthor(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope()
               .ServiceProvider.GetRequiredService<IdentityContext>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                await context.Database.MigrateAsync();

            var existingAuthors = await context.Authors.ToListAsync();
            if (existingAuthors != null) return;

            var authors = new List<Author>
            {
                new Author
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    FirstName = "Harry",
                    LastName = "Wills",
                    Email = "harrywills@domain.com",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1992, 11, 11),
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Title = "Success Comes At A Price",
                            ISBN = "56422299875",
                            YearPublished = new DateTime(2006, 12, 23),
                            AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Category = "Career"
                        },

                        new Book
                        {
                            Id = new Guid("02068a01-69f5-4b6d-f678-08d98a148f09"),
                            Title = "Toolkit for Mentorship",
                            ISBN = "234977423470",
                            YearPublished = new DateTime(2018, 2, 18),
                            AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Category =  "Career"
                        }
                    },
                    CreatedBy = "Gideon"
                },

                new Author
                {
                    Id = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                    FirstName = "Terry",
                    LastName = "Graham",
                    Email = "terrygraham@domain.com",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1995, 3, 1),
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Id = new Guid("63ab3e7c-c817-4fdb-85f8-08d98a1767fe"),
                            Title = "Principles of Expansion",
                            ISBN = "882457625741",
                            YearPublished = new DateTime(2008, 8, 13),
                            AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                            Category =  "Sales"
                        },

                        new Book
                        {
                            Id = new Guid("e87d9b47-fc47-4e89-4ab3-08d98a1e48fe"),
                            Title = "Keys of Networking",
                            ISBN = "536648497957",
                            YearPublished = new DateTime(2017, 9, 15),
                            AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
                            Category =  "Sales"
                        }
                    },
                    CreatedBy = "Gideon"
                }
            };

            //var author1 = new Author
            //{
            //    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            //    FirstName = "Harry",
            //    LastName = "Wills",
            //    Email = "harrywills@domain.com",
            //    Gender = Gender.Male,
            //    DateOfBirth = new DateTime(1992, 11, 11),
            //    Books = new List<Book>
            //    {
            //        new Book
            //        {
            //            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
            //            Title = "Success Comes At A Price",
            //            ISBN = "56422299875",
            //            YearPublished = new DateTime(2006, 12, 23),
            //            AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            //            Category = new List<string>{ "Motivation", "Career"}
            //        },

            //        new Book
            //        {
            //            Id = new Guid("02068a01-69f5-4b6d-f678-08d98a148f09"),
            //            Title = "Toolkit for Mentorship",
            //            ISBN = "234977423470",
            //            YearPublished = new DateTime(2018, 2, 18),
            //            AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            //            Category = new List<string> { "Non-fiction", "Career" }
            //        }
            //    },
            //    CreatedBy = "Gideon"
            //};

            //var author2 = new Author
            //{
            //    Id = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
            //    FirstName = "Terry",
            //    LastName = "Graham",
            //    Email = "terrygraham@domain.com",
            //    Gender = Gender.Male,
            //    DateOfBirth = new DateTime(1995, 3, 1),
            //    Books = new List<Book>
            //    {
            //        new Book
            //        {
            //            Id = new Guid("63ab3e7c-c817-4fdb-85f8-08d98a1767fe"),
            //            Title = "Principles of Expansion",
            //            ISBN = "882457625741",
            //            YearPublished = new DateTime(2008, 8, 13),
            //            AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
            //            Category = new List<string> { "Sales", "Career" }
            //        },

            //        new Book
            //        {
            //            Id = new Guid("e87d9b47-fc47-4e89-4ab3-08d98a1e48fe"),
            //            Title = "Keys of Networking",
            //            ISBN = "536648497957",
            //            YearPublished = new DateTime(2017, 9, 15),
            //            AuthorId = new Guid("fdb13789-066c-4a24-bbf9-08d98a14c243"),
            //            Category = new List<string> { "Sales", "Career" }
            //        }
            //    },
            //    CreatedBy = "Gideon"
            //};

            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        public static async void SeedCategory(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope()
               .ServiceProvider.GetRequiredService<IdentityContext>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
                await context.Database.MigrateAsync();

            var existingCategories = await context.Categories.ToListAsync();
            if (existingCategories != null) return;

            var categories = new List<Category>
            {
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
                }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        private static async Task CreateRole(string name)
        {
            if (!await _roleManager.RoleExistsAsync(name))
            {
                var role = new Role { Name = name };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
