using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Domain.Entities;

public class Emprunt : BaseEntity
{
  //FK
  public Guid LivreId { get; set; }
  public Guid UserId { get; set; }
  
  //données emprunt

  public DateTime DateEmprunt { get; set; }
  public DateTime? DateRetourPrevu { get; set; }
  public DateTime? DateRetourEffective { get; set; }
  
  //navigation
  
  public virtual Livre Livre { get; set; } = null!;
  public virtual User? User { get; set; } = null!;
}