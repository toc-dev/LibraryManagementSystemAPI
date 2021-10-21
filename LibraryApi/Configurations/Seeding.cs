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
        private const string User1 = "g.akunana";
        private const string User1Password = "Gideon@12345678";
        private const string User2 = "t.sage";
        private const string User2Password = "Sage@12345678";
        private const string User3 = "k.michael";
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
