using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibraryManager.Infrastructure.DataBase;

public class LibraryManagerContextFactory
  : IDesignTimeDbContextFactory<LibraryManagerContext>
{
  //FAIT PAR CLAUDE AVANT D'ARRETER CAR LA FLEMME
  
  
  public LibraryManagerContext CreateDbContext(string[] args)
  {
    // 1️⃣ Charger la configuration
    IConfiguration configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .AddJsonFile("appsettings.Development.json", optional: true)
      .Build();

    // 2️⃣ Récupérer la connection string
    var connectionString = configuration.GetConnectionString("Default");

    // 3️⃣ Construire les options
    var optionsBuilder = new DbContextOptionsBuilder<LibraryManagerContext>();
    optionsBuilder.UseSqlServer(connectionString);

    // 4️⃣ Créer le DbContext
    return new LibraryManagerContext(optionsBuilder.Options); 
  }
}