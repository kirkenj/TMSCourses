using Microsoft.EntityFrameworkCore;
using WebApiDatabase;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;
using WebApi.Models.Interfaces;
using WebApi.Models.Services;
using WebApi.MiddleWares;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AutoparkDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAutoparkDBContext, AutoparkDBContext>();

builder.Services.AddTransient<IDeleteService<int>, CarDeleteService>();
builder.Services.AddTransient<IUpdateService<CarUpdateModel>, CarUpdateService>();
builder.Services.AddTransient<IGetService<CarItemModel>, CarGetService>();
builder.Services.AddTransient<ICreateService<CarCreateModel>, CarCreateService>();

builder.Services.AddTransient<IDeleteService<int>, CarTypeDeleteService>();
builder.Services.AddTransient<IUpdateService<CarTypeUpdateModel>, CarTypeUpdateService>();
builder.Services.AddTransient<IGetService<CarTypeItemModel>, CarTypeGetService>();
builder.Services.AddTransient<ICreateService<CarTypeCreateModel>, CarTypeCreateService>();

builder.Services.AddTransient<IDeleteService<int>, ParkingPlaceDeleteService>();
builder.Services.AddTransient<IUpdateService<ParkingPlaceUpdateModel>, ParkingPlaceUpdateService>();
builder.Services.AddTransient<IGetService<ParkingPlaceItemModel>, ParkingPlaceGetService>();
builder.Services.AddTransient<ICreateService<int>, ParkingPlaceCreateService>();

builder.Services.AddTransient<IDeleteService<int>, ClientDeleteService>();
builder.Services.AddTransient<IUpdateService<ClientUpdateModel>, ClientUpdateService>();
builder.Services.AddTransient<IGetService<ClientItemModel>, ClientGetService>();
builder.Services.AddTransient<ICreateService<ClientCreateModel>, ClientCreateService>();

builder.Services.AddTransient<ICreateService<ClientCreateModel>, ClientCreateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionCatchingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
