using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.LivreRequest;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Interfaces.Services;

public interface ILivreService : IBaseService<Livre>
{
  Task<IEnumerable<Livre>> GetLivreDispoAsync();
  Task<Livre>CreateFromDtoAsync(LivreRequestDTO livre);
  
}