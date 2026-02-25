using LibraryManager.Core.DTOs.Responces.EmpruntResponseDto;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Interfaces.Services;

public interface IEmpruntService : IBaseService<Emprunt>
{
  Task<EmpruntResponseDto> EmprunterLivreAsync(Guid livreId, Guid userId);
  Task<EmpruntResponseDto> RendreLivreAsync(Guid empruntId);
}