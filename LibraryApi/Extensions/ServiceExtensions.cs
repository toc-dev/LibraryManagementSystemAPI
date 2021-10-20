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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRegisterLoginService, RegisterLoginService>();
            services.AddTransient<IUnitOfWork<IdentityContext>, UnitOfWork<IdentityContext>>();
            return services;
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new 
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }
    }
}
