namespace LibraryManager.Core.DTOs.Requests.UserRequest;

public class LoginRequestDto
{
  public required string Email { get; set; } = string.Empty;
  public required string PasswordHash { get; set; } =  string.Empty;
}