using Microsoft.EntityFrameworkCore;
using Serilog;
using SuperHeroAPI.DAL.Context;
using SuperHeroAPI.Services.Interfaces;
using SuperHeroAPI.Services.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration));

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Регистрация собственный сервисов
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();
builder.Services.AddScoped<ISuperHeroTeamRepository, SuperHeroTeamRepository>();
builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
