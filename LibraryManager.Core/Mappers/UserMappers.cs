using LibraryManager.Core.DTOs.Requests.UserRequest;
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
      PasswordHash = string.Empty,
      FirstName = dto.FirstName ?? string.Empty,
      LastName = dto.LastName ?? string.Empty,
    };
  }

  public static UserResponceDto ToResponseDto(this User entity)
  {
    return new UserResponceDto
    {
      Id = entity.Id,
      Email = entity.Email,
      FirstName = entity.FirstName ?? string.Empty,
      LastName = entity.LastName ?? string.Empty,
    };
  }
}