using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Repositories.Configurations;

public class EmpruntConfiguration : IEntityTypeConfiguration<Emprunt>
{
  public void Configure(EntityTypeBuilder<Emprunt> builder)
  {
    builder.HasKey(e => e.Id);
    
    builder.Property(e => e.DateEmprunt ).IsRequired().HasDefaultValueSql("GETDATE()");
    builder.Property(e => e.DateRetourPrevu).IsRequired();
    builder.Property(e => e.DateRetourEffective).IsRequired(false);
    
    builder.HasOne(e => e.Livre)
      .WithMany(l => l.HistoriqueEmprunts)
      .HasForeignKey(e => e.LivreId)
      .OnDelete(DeleteBehavior.Restrict);
    
    builder.HasOne(e => e.User)
      .WithMany(u => u.Emprunts)
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}