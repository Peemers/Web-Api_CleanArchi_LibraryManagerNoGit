using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.DataBase;

public class LibraryManagerContext : DbContext
{
  public LibraryManagerContext(DbContextOptions<LibraryManagerContext> options)
    : base(options)
  {
  }

  public DbSet<Livre> Livres { get; set; } = null!;
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Emprunt> Emprunt { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryManagerContext).Assembly);
  }
}