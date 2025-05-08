using Microsoft.EntityFrameworkCore;
using ProniaOneToManyFileCRUD.DAL;
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
