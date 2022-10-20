using Microsoft.EntityFrameworkCore;
using WebApiDatabase;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;
using WebApi.Models.Interfaces;
using WebApi.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
