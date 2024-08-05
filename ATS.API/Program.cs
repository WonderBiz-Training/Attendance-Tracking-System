using ATS.Data;
using ATS.IRepository;
using ATS.IServices;
using ATS.Repository;
using ATS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ATSDbContext>(option => option.UseSqlServer(connectionString, b => b.MigrationsAssembly("ATS.API")));

builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<IDesignationServices, DesignationServices>();

// Add services to the container.


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
