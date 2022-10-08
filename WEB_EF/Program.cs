using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WEB_EF.Models.DBContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AutoparkContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Journal}/{action=Index}/{id?}");

app.Run();
//Scaffold-DbContext "Server=DESKTOP-9VGHPR7\SQLEXPRESS;Database=Autopark;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
//Scaffold -DbContext "Server=DESKTOP-9VGHPR7\SQLEXPRESS;Database=Autopark;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models"
//Scaffold - DbContext "*connection*" "*provider*" - OutputDir "BackendProject" - ContextDir "DbContexts"