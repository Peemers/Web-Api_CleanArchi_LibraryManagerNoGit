using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;

public class Livre : BaseEntity
{
  public string Nom { get; set; } = string.Empty;
  public string Auteur { get; set; } = string.Empty;
  public int NbPages { get; set; }
  public string Resume { get; set; } = string.Empty;
  public DateTime DateDeSortie { get; set; }
  public LivreStatut StatutLivre { get; set; }
  public string? UrlCouverture { get; set; }

  public virtual ICollection<Emprunt> HistoriqueEmprunts { get; set; } = new List<Emprunt>();
}