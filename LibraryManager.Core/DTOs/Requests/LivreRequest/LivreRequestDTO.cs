using System.ComponentModel.DataAnnotations;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.DTOs.Requests.LivreRequest;

public class LivreRequestDTO
{
  [Required]
  [MinLength(2, ErrorMessage = "Minimum 2 Caracteres")]
  [MaxLength(64, ErrorMessage = "Maximum 64 Caracteres")]
  public required string Nom { get; set; }
  
  [Required]
  [MinLength(2, ErrorMessage = "Minimum 2 Caracteres")]
  [MaxLength(64, ErrorMessage = "Maximum 64 Caracteres")]
  public required string Auteur { get; set; }
  
  [Required]
  [MinLength(12, ErrorMessage = "Minimum 12 Caracteres")]
  [MaxLength(240, ErrorMessage = "Maximum 240 Caracteres")]
  public required string Resume { get; set; }
  
  [Required]
  public int NbPages { get; set; }
  
  [Required]
  public DateTime DateDeSortie { get; set; }
  
  [Required]
  public LivreStatut StatutLivre { get; set; }
  
  public string? UrlCouverture { get; set; } = string.Empty;
}