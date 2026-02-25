namespace LibraryManager.Core.DTOs.Responces.EmpruntResponseDto;

public class EmpruntResponseDto
{
  public Guid Id { get; set; }
  public Guid LivreId { get; set; }
  public Guid UserId { get; set; }
  public DateTime DateEmprunt { get; set; }
  public DateTime? DateRetourPrevu { get; set; }
  public DateTime? DateRetourEffective { get; set; }
}