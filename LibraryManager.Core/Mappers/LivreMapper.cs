using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.LivreRequest;
using LibraryManager.Core.DTOs.Responces;
using LibraryManager.Core.DTOs.Responces.LivreResponse;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.Mappers;

public static class LivreMapper
{
  // Sens : API -> SERVICE
  public static Livre ToEntity(this LivreRequestDTO dto)
  {
    return new Livre
    {
      Nom = dto.Nom,
      Auteur = dto.Auteur,
      NbPages = dto.NbPages,
      Resume = dto.Resume,
      DateDeSortie = dto.DateDeSortie,
    };
  }

  // Sens : SERVICE -> API
  public static LivreResponceDto ToResponseDto(this Livre entity)
  {
    return new LivreResponceDto
    {
      Nom = entity.Nom,
      Auteur = entity.Auteur,
      NbPages = entity.NbPages,
      DateDeSortie =  entity.DateDeSortie,
      DateCreation =  entity.DateCreation,
      DateModification =  entity.DateModification ??  DateTime.UtcNow,
      StatutLivre = entity.StatutLivre
    };
  }
}