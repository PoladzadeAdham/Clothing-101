using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplication3.Model;

namespace WebApplication3.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   

            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
