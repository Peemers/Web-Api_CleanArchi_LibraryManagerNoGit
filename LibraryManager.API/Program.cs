using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using LibraryManager.Infrastructure.DataBase;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Services.Data;
using LibraryManager.Infrastructure.Repositories; // Vérifie que LivreService est bien ici

var builder = WebApplication.CreateBuilder(args);

// 1. Base de données
builder.Services.AddDbContext<LibraryManagerContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//Injection de dépendances

builder.Services.AddScoped<ILivreRepository, LivreRepository>();
builder.Services.AddScoped<ILivreService, LivreService>();
builder.Services.AddScoped<IEmpruntRepository, EmpruntRepository>();
builder.Services.AddScoped<IEmpruntService, EmpruntService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
Console.WriteLine($"CORS configurés pour : {string.Join(", ", allowedOrigins ?? [])}");

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngular", policy =>
  {
    policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

//Services de base
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

//Pipeline HTTP (Middleware)
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi(); //creation json
  app.MapScalarApiReference();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/test", () => "API OK");



app.Run();