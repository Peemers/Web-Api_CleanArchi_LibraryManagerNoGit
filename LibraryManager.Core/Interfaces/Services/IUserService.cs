using LibraryManager.Core.DTOs.Requests.UserRequest;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.Interfaces.Services;

public interface IUserService : IBaseService<User>
{
  new Task<IEnumerable<UserResponceDto>> GetAllAsync();
  Task<UserResponceDto?> GetByEmailAsync(string email);
  Task<UserResponceDto> CreateByDtoAsync(RegisterRequestDto dto);
  Task<UserResponceDto> UpdateEmailAsync(Guid userId, string newEmail);
  Task<UserResponceDto> ChangeRoleAsync(Guid userId, UsersRoles role);
}