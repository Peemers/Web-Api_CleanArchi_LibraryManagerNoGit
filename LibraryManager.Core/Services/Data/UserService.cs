using System.Text.RegularExpressions;
using LibraryManager.Core.Common;
using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.UserRequest;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Interfaces.Tools;
using LibraryManager.Core.Mappers;
using LibraryManager.Core.Services.Tools;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Core.Services.Data;

public class UserService(
  IUserRepository userRepository,
  IPasswordHasher passwordHasher,
  IJwtProvider jwtProvider) : BaseService<User>(userRepository), IUserService
{
  public async Task<UserResponceDto?> GetByEmailAsync(string email)
  {
    var user = await userRepository.GetUserByEmail(email);
    return user?.ToResponseDto();
  }

  public override async Task<IEnumerable<User>> GetAllAsync()
  {
    var users = await base.GetAllAsync();
    return users.OrderBy(u => u.LastName).ToList();
  }


  async Task<IEnumerable<UserResponceDto>> IUserService.GetAllAsync()
  {
    var users = await this.GetAllAsync();
    return users.Select(u => u.ToResponseDto());
  }

  public async Task<UserResponceDto> CreateByDtoAsync(RegisterRequestDto dto)
  {
    // LOG DE CONTRÔLE
    Console.WriteLine($"--- INSCRIPTION ---");
    Console.WriteLine($"Email à inscrire : {dto.Email}");
    Console.WriteLine($"Mot de passe reçu à hacher : {dto.Password}");

    if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
    {
      throw new ArgumentException("un mail et un mdp sont nécessaires");
    }

    var userEntity = dto.ToEntity();
    
    // On s'assure qu'on hache bien ce qu'on a reçu
    string hashed = passwordHasher.Hash(dto.Password)!;
    Console.WriteLine($"Hash généré pour la base : {hashed}");
    
    userEntity.PasswordHash = hashed;
    
    var createdUser = await base.AddAsync(userEntity);
    return createdUser.ToResponseDto();
  }

  public async Task<UserResponceDto> ChangeRoleAsync(Guid userId, UsersRoles role)
  {
    var user = await base.GetByIdAsync(userId);
    if (role == UsersRoles.Admin)
    {
      if (user == null)
      {
        throw new Exception("User introuvable");
      }

      if (user.Role != role)
      {
        user.Role = role;
        await base.UpdateAsync(user);
      }
    }
    
    return user!.ToResponseDto();
  }

  public async Task<Result<LoginResponceDto>> LoginAsync(LoginRequestDto dto)
  {
    User? user = await userRepository.GetUserByEmail(dto.Email);

    if (user == null)
    {
      return Result<LoginResponceDto>.Failure("Email ou MDP incorrect");
    }

    bool isPasswordValid = passwordHasher.Verify(dto.Password, user.PasswordHash);
  
    if (!isPasswordValid)
    {
      return Result<LoginResponceDto>.Failure("Email ou MDP incorrect");
    }

    string token = jwtProvider.Generate(user);
  
    LoginResponceDto loginResponse = new LoginResponceDto
    {
      User = user.ToResponseDto(),
      Token = token
    };

    return Result<LoginResponceDto>.Success(loginResponse);
  }

  public async Task<UserResponceDto> UpdateEmailAsync(Guid userId, string nouveauEmail)
  {
    if (string.IsNullOrWhiteSpace(nouveauEmail))
      throw new ArgumentException("L'email ne peut pas être vide.");

    if (!Regex.IsMatch(nouveauEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
      throw new ArgumentException("Format d'email invalide.");

    var user = await GetByIdAsync(userId);
    if (user == null)
      throw new Exception("Utilisateur introuvable.");

    var existingUser = await GetByEmailAsync(nouveauEmail);
    if (existingUser != null && existingUser.Id != userId)
      throw new ArgumentException("Cet email est déjà utilisé.");

    user.Email = nouveauEmail;

    await UpdateAsync(user);

    return user.ToResponseDto();
  }
}