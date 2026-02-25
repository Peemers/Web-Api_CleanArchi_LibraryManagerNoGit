using Microsoft.EntityFrameworkCore.Design;

namespace LibraryManager.Infrastructure.DataBase.Persistence;

public class LibraryManagerContextFactory : IDesignTimeDbContextFactory<LibraryManagerContext>
{
  public LibraryManagerContext CreateDbContext(string[] args)
  {
    var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "../LibraryManager.Api");
  }
}
