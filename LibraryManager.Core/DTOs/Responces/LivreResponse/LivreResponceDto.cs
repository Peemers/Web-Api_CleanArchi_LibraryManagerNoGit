using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.DTOs.Responces.LivreResponse;

public class LivreResponceDto
{
  
  public required string Nom { get; set; } 
  public required string Auteur { get; set; }
  public int NbPages { get; set; }
  public DateTime DateDeSortie { get; set; }
  public DateTime DateCreation { get; set; }
  public LivreStatut StatutLivre { get; set; }
  public DateTime? DateModification  { get; set; }
}