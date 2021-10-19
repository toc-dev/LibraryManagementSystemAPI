using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibraryApi.Services.Implementations;
using LibraryApi.Services.Interfaces;
using LibraryApi.Data.Implementations;
using LibraryApi.Data.Interfaces;

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
            var builder = services.AddIdentityCore<User>(u =>
            {
                u.Password.RequireDigit = true;
                u.Password.RequireLowercase = true;
                u.Password.RequireUppercase = true;
                u.Password.RequireNonAlphanumeric = true;
                u.Password.RequiredLength = 10;
                u.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
                builder.Services);
            builder.AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<DbContext, IdentityContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork<IdentityContext>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRegisterLoginService, RegisterLoginService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
