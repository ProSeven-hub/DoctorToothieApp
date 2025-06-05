using DoctorToothieApp.Persistence;
using DoctorToothieApp.Persistence.Models;
using DoctorToothieApp.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DoctorToothieApp.Persistence.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<IDbContext, ApplicationDbContext>();
        builder.Services.AddScoped<IDbContext, ApplicationDbContext>();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ToothieDb")
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddRazorPages();
        builder.Services.AddIdentity<AppUser, IdentityRole>(
            options => options.SignIn.RequireConfirmedAccount = false
        )
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddApiEndpoints();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            SeedDatabase(app);
            app.UseMigrationsEndPoint();

        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();


        app.UseAuthorization();


        //app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
    private static void SeedDatabase(IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            // To jest inmemory, nie moge zrobic migracji. Jak zmienimy na sql to bedzie dzialac
            //context.Database.Migrate();
            // Seed danych (Role i Admin)
            SeedRolesAndAdmin(context);
        }
    }

    private static void SeedRolesAndAdmin(ApplicationDbContext context)
    {
        if (!context.Roles.Any())
        {
            context.SeedRole(); 
        }

        if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
        {
            context.SeedAdminUser(); 
        }
    }
}