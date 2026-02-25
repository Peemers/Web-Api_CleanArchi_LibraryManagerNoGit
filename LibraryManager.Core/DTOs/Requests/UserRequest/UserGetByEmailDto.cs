namespace LibraryManager.Core.DTOs.Requests.UserRequest;

public class UserGetByEmailDto
{
  public Guid Id { get; set; }
  public required string Email { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
}