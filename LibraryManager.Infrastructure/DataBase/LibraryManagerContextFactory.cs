using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

//factory proposee par ia explication syntetisée dans le obsidian "FactoryContext"

namespace LibraryManager.Infrastructure.DataBase;

public class LibraryManagerContextFactory
  : IDesignTimeDbContextFactory<LibraryManagerContext>
{
  
  public LibraryManagerContext CreateDbContext(string[] args)
  {
    var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "../LibraryManager.API");
    
    IConfiguration configuration = new ConfigurationBuilder()
      .SetBasePath(apiPath)
      .AddJsonFile("appsettings.json")
      .AddJsonFile("appsettings.Development.json", optional: true)
      .Build();

    
    var connectionString = configuration.GetConnectionString("Default");

    
    var optionsBuilder = new DbContextOptionsBuilder<LibraryManagerContext>();
    optionsBuilder.UseSqlServer(connectionString);

    
    return new LibraryManagerContext(optionsBuilder.Options); 
  }
}
