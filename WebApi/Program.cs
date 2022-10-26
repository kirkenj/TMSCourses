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

builder.Services.AddTransient<IDeleteService<int>, CarCRUDLService>();
builder.Services.AddTransient<IUpdateService<CarUpdateModel>, CarCRUDLService>();
builder.Services.AddTransient<IGetService<CarItemModel>, CarCRUDLService>();
builder.Services.AddTransient<ICreateService<CarCreateModel>, CarCRUDLService>();

builder.Services.AddTransient<IDeleteService<int>, CarTypeCRUDLService>();
builder.Services.AddTransient<IUpdateService<CarTypeUpdateModel>, CarTypeCRUDLService>();
builder.Services.AddTransient<IGetService<CarTypeItemModel>, CarTypeCRUDLService>();
builder.Services.AddTransient<ICreateService<CarTypeCreateModel>, CarTypeCRUDLService>();

builder.Services.AddTransient<IDeleteService<int>, ParkingPlaceCRUDLService>();
builder.Services.AddTransient<IUpdateService<ParkingPlaceUpdateModel>, ParkingPlaceCRUDLService>();
builder.Services.AddTransient<IGetService<ParkingPlaceItemModel>, ParkingPlaceCRUDLService>();
builder.Services.AddTransient<ICreateService<int>, ParkingPlaceCRUDLService>();

builder.Services.AddTransient<IDeleteService<int>, ClientCRUDLService>();
builder.Services.AddTransient<IUpdateService<ClientUpdateModel>, ClientCRUDLService>();
builder.Services.AddTransient<IGetService<ClientItemModel>, ClientCRUDLService>();
builder.Services.AddTransient<ICreateService<ClientCreateModel>, ClientCRUDLService>();

builder.Services.AddTransient<IDeleteService<int>, JournalCRUDLService>();
builder.Services.AddTransient<IUpdateService<JournalUpdateModel>, JournalCRUDLService>();
builder.Services.AddTransient<IGetService<JournalItemModel>, JournalCRUDLService>();
builder.Services.AddTransient<ICreateService<JournalCreateModel>, JournalCRUDLService>();

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
