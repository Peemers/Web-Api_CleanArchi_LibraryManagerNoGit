using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Infrastructure.Repositories.Configurations;

public class LivreConfiguration : IEntityTypeConfiguration<Livre>
{
  public void Configure(EntityTypeBuilder<Livre> builder)
  {
    builder.HasKey(livre => livre.Id);
    builder.Property(livre => livre.Nom).IsRequired().HasMaxLength(200);
    builder.Property(livre => livre.Auteur).IsRequired().HasMaxLength(100);

    builder.Property(livre => livre.StatutLivre)
      .IsRequired()
      .HasColumnType("int")
      .HasDefaultValue(LivreStatut.Disponible);
      
    
    builder.Property(livre => livre.DateCreation).HasDefaultValueSql("GETDATE()");
  }
}