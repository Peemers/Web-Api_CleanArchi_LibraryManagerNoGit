using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Repositories;

public class UserRepository (LibraryManagerContext _context) : BaseRepository<User>(_context), IUserRepository
{
  public async Task<User?> GetUserByEmail(string email)
  {
    return await _context.Users
      .FirstOrDefaultAsync(u => u.Email == email);
  }
}