using Microsoft.EntityFrameworkCore;
using WebApi.Models.DBContexts;
using WebApi.Models.Interfaces;
using WebApi.Models.Services;
using WebApi.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AutoparkDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAutoparkDBContext, AutoparkDBContext>();
builder.Services.AddTransient<ICRUDlService<Car>, CarCrudlService>();
builder.Services.AddTransient<ICRUDlService<CarType>, CarTypeCrudlService>();
builder.Services.AddTransient<ICRUDlService<Client>, ClientCrudlService>();
builder.Services.AddTransient<ICRUDlService<Journal>, JournalCrudlService>();
builder.Services.AddTransient<IValidateService<Journal>, JournalValidateService>();
builder.Services.AddTransient<ICRUDlService<ParkingPlace>, ParkingPlaceCrudlService>();
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