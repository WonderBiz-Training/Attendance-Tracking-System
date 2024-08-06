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

builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IGenderServices, GenderServices>();

builder.Services.AddScoped<IAttendanceLogRepository, AttendanceLogRepository>();
builder.Services.AddScoped<IAttendanceLogServices, AttendanceLogServices>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IEmployeeDetailRepository, EmployeeDetailRepository>();
builder.Services.AddScoped<IEmployeeDetailServices, EmployeeDetailServices>();

// Add services to the container.


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
