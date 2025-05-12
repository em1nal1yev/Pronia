using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOneToManyFileCRUD.DAL;
using ProniaOneToManyFileCRUD.Models;
using System.Configuration;


namespace ProniaOneToManyFileCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 3;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            
            app.UseStaticFiles();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute("Default", "{Controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
