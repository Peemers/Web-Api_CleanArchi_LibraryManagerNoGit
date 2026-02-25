using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Repositories;

public class BaseRepository<T>(LibraryManagerContext _context) : IBaseRepository<T> where T : BaseEntity
{
  protected DbSet<T> _entities => _context.Set<T>();

  public async Task<T> AddAsync(T entity)
  {
    await _entities.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task DeleteAsync(Guid id)
  {
    var entity = await _entities.FindAsync(id);
    {
      _entities.Remove(entity);
      await _context.SaveChangesAsync();
    }
  }


  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _entities.ToListAsync();
  }

  public async Task<T?> GetByIdAsync(Guid id)
  {
    return await _entities.FindAsync(id);
  }

  public async Task<T> UpdateAsync(T entity)
  {
    // Force l'entrée de l'entité dans le tracker comme "Modified"
    var entry = _context.Entry(entity);
    entry.State = EntityState.Modified;
    
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> ExistsAsync(Guid id)
  {
    return await Task.FromResult(_entities.Find(id) != null);
  }
}