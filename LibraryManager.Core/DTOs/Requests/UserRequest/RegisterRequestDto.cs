namespace LibraryManager.Core.DTOs.Requests.UserRequest;

public class RegisterRequestDto
{
  public required string Email { get; set; } = string.Empty;
  public required string PasswordHash { get; set; } =  string.Empty;

  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
}