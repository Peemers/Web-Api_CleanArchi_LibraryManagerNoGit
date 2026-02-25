using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.DataBase;

namespace LibraryManager.Infrastructure.Repositories;

public class EmpruntRepository : BaseRepository<Emprunt> , IEmpruntRepository
{
  public EmpruntRepository(LibraryManagerContext context) : base(context)
  {
  }
  
}