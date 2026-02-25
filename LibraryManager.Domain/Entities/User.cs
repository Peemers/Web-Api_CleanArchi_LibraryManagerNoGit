

using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;


public class User : BaseEntity
{
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } =  string.Empty;
  public UsersRoles Role { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  
  //navigation

  public virtual ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
}