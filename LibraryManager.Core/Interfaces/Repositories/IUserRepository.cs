using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
  Task<User?> GetUserByEmail(string email);
}