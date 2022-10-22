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
builder.Services.AddTransient<IGetService2<Car, CarItemModel>, CarGetService>();
builder.Services.AddTransient<ICreateService<CarCreateModel>, CarCreateService>();
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

app.UseMiddleware<ExceptionCatchingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
