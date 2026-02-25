using LibraryManager.Domain.Entities;

namespace LibraryManager.Core.Interfaces.Services;

public interface IBaseService<T> where T : BaseEntity
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(Guid id);
  
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task DeleteAsync(Guid id);
}