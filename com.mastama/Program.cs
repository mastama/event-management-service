using com.mastama.Configuration;
using com.mastama.Models;
using com.mastama.Repositories.Implementations;
using com.mastama.Repositories.Interfaces;
using com.mastama.Services.Implementations;
using com.mastama.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Kirim log ke konsol
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Kirim log ke file
    .CreateLogger();

builder.Host.UseSerilog(); // Gunakan Serilog sebagai logger utama

// Layanan untuk Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrasi service dan dependensi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Configure<ServiceIdConfig>(builder.Configuration.GetSection("Service"));

// Layanan untuk Controller
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();