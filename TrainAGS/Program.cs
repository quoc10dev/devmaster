using Microsoft.EntityFrameworkCore;
using TrainAGS.Models;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
        builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        /*IdentityBuilder identityBuilder = builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddEntityFrameworkStores<ApplicationDbContext>(); */

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllerRoute(
             name: "Areas",
             pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}