using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
  //le crud de base
  
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(Guid id);
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task DeleteAsync(Guid id);
  
  Task<bool> ExistsAsync(Guid id);
}