using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.DTOs.Requests.LivreRequest;

public class LivreRequestDTO
{
  public required string Nom { get; set; }
  public required string Auteur { get; set; }
  public int NbPages { get; set; }
  public DateTime DateDeSortie { get; set; }
}