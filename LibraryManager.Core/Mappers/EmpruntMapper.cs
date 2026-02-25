using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.EmpruntRequestDto;
using LibraryManager.Core.DTOs.Responces.EmpruntResponseDto;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Mappers;

public static class EmpruntMapper
{
  public static Emprunt ToEntity(this EmpruntRequestDto dto)
  {
    return new Emprunt
    {
      LivreId = dto.LivreId,
      UserId = dto.UserId,
      DateEmprunt = DateTime.UtcNow,
      DateRetourPrevu =  DateTime.UtcNow.AddDays(14)
    };
  }
  public static EmpruntResponseDto ToDto(this Emprunt entity)
  {
    return new EmpruntResponseDto
    {
      Id = entity.Id,
      LivreId = entity.LivreId,
      UserId = entity.UserId,
      DateEmprunt = entity.DateEmprunt,
      DateRetourPrevu = entity.DateRetourPrevu,
      DateRetourEffective = entity.DateRetourEffective,
    };
  }
}

