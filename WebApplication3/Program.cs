using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication3.Context;
using WebApplication3.Model;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(

                option =>
                {
                    option.Password.RequiredLength = 4;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = true;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireDigit = false;
                }

                )
                .AddEntityFrameworkStores<AppDbContext>();


            var app = builder.Build();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.UseStaticFiles();

            app.Run();
        }
    }
}
