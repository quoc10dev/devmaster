using CMS.DatabaseContext;
using CMS.ImplementClass;
using CMS.Interface;
using CMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; //Khong bat nhap so
    options.Password.RequireNonAlphanumeric = false; //Khong bat nhap ky tu dat biet
    options.Password.RequireUppercase = false; //Khong bat nhap in hoa
});
var CMSConnString = builder.Configuration.GetConnectionString("CMSConnString");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(CMSConnString);
});
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(CMSConnString);
});
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/app/login";
    config.AccessDeniedPath = "/error/accessdenied";
    config.SlidingExpiration = false;
    config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});
builder.Services.Configure<UploadPath>(builder.Configuration.GetSection("UploadPath"));
var serverDirPath = builder.Configuration.GetSection("UploadPath").GetSection("ServerDirPath").Value;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(serverDirPath, "Upload")),
    RequestPath = "/Upload"
});
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
