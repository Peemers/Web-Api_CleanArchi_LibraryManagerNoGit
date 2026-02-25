using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.DataBase;

namespace LibraryManager.Infrastructure.Repositories;

public class LivreRepository(LibraryManagerContext _context) : BaseRepository<Livre>(_context), ILivreRepository
{
  
}