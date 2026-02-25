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
    var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "../LibraryManager.Api");
    
    IConfiguration configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .AddJsonFile("appsettings.Development.json", optional: true)
      .Build();

    
    var connectionString = configuration.GetConnectionString("Default");

    
    var optionsBuilder = new DbContextOptionsBuilder<LibraryManagerContext>();
    optionsBuilder.UseSqlServer(connectionString);

    
    return new LibraryManagerContext(optionsBuilder.Options); 
  }
}