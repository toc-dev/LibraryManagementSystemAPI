using LibraryApi.Models.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class IdentityContext : IdentityDbContext<User, Role, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity => entity.ToTable(name: "Users"));

            builder.Entity<Role>(entity => entity.ToTable(name: "Roles"));

            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));

            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));

            builder.ApplyConfiguration<Category>(new CategoryConfiguration());
            builder.ApplyConfiguration<Author>(new AuthorConfiguration());
            builder.ApplyConfiguration<Book>(new BookConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
