using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.UserRequest;
using LibraryManager.Core.DTOs.Responces;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Mappers;

public static class UserMappers
{
  public static User ToEntity(this RegisterRequestDto dto)
  {
    return new User
    {
      Email = dto.Email,
      PasswordHash = dto.PasswordHash,
      FirstName = dto.FirstName ?? string.Empty,
      LastName = dto.LastName ?? string.Empty,
    };
  }

  public static UserResponceDto ToResponseDto(this User dto)
  {
    return new UserResponceDto
    {
      Id = dto.Id,
      Email = dto.Email,
      FirstName = dto.FirstName,
      LastName = dto.LastName,
    };
  }
}