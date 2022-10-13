using Microsoft.EntityFrameworkCore;
using WEB_EF.Models.DBContexts;
using WEB_EF.Models.Interfaces;
using WEB_EF.Models.Services;
using WEB_EF.Models.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AutoparkDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAutoparkDBContext, AutoparkDBContext>();
builder.Services.AddTransient<ICRUDlService<Car>, CarCrudlService>();
builder.Services.AddTransient<ICRUDlService<CarType>, CarTypeCrudlService>();
builder.Services.AddTransient<ICRUDlService<Client>, ClientCrudlService>();
builder.Services.AddTransient<ICRUDlService<Journal>, JournalCrudlService>();
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