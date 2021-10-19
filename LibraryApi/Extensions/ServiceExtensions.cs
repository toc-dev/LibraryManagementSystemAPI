using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryApi.Models.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibraryApi.Data.Interfaces;
using LibraryApi.Data.Implementations;
using LibraryApi.Services.Interfaces;
using LibraryApi.Services.Implementations;

namespace LibraryApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"),
                    b => b.MigrationsAssembly("LibraryApi"));
            });
            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<DbContext, IdentityContext>();
            services.AddTransient<IUnitOfWork<IdentityContext>, UnitOfWork<IdentityContext>>();
            services.AddTransient<IServiceFactory, ServiceFactory>();

            /* Caution: 
            *   Having issues with add-migration and update-databse
            *   whenever I add these services.
            *   I don't have an idea yet,
            *   Maybe cos we already using the servicefactory when to get other services.
            *   So that why I commented it out for now.
            */
            //services.AddTransient<IBookService, BookService>();
            //services.AddTransient<IAuthorService, AuthorService>();
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<ICategoryService, CategoryService>();
            return services;
        }
    }
}
