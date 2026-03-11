using System.Text;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using LibraryManager.Infrastructure.DataBase;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Interfaces.Tools;
using LibraryManager.Core.Services.Data;
using LibraryManager.Core.Services.Tools;
using LibraryManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events; // Vérifie que LivreService est bien ici


Log.Logger = new LoggerConfiguration() //creation du loger avant tout le reste
  .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //le niveau d'info
  .Enrich.FromLogContext() //coontexte
  .WriteTo.Console() //ou ça va etre ecrit
  .CreateBootstrapLogger();

try
{
  var builder = WebApplication.CreateBuilder(args);

//Base de données
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
      policy.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
  });

//Services de base
  builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; });
  builder.Services.AddOpenApi();
  builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
  builder.Services.AddScoped<IJwtProvider, JwtProvider>();
  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
      };
    });

  var app = builder.Build();

//Pipeline HTTP (Middleware)
  if (app.Environment.IsDevelopment())
  {
    app.MapOpenApi(); //creation json
    app.MapScalarApiReference();
  }

  app.UseCors("AllowAngular");
  app.UseHttpsRedirection();
  app.UseAuthentication();
  app.UseAuthorization();
  app.MapControllers();
  app.MapGet("/test", () => "API OK");



  app.Run();
}
catch (Exception ex)
{

}
finally
{
  
}