namespace LibraryManager.Domain.Entities;

public abstract class BaseEntity
{
  public Guid Id { get; set; }
  public DateTime DateCreation { get; set; } = DateTime.UtcNow;
  public DateTime? DateModification { get; set; }
  
  protected  BaseEntity()
  {
    Id = Guid.NewGuid();
  }
}