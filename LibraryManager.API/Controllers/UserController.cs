using LibraryManager.Core.Common;
using LibraryManager.Core.DTOs.Requests.UserRequest;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase //prim ctor
{
  // private IUserService _userService;
  //
  // public UserController(IUserService userService)
  // {
  //   _userService = userService;
  // } commenté pour le primary Ctor je garde pour l'exemple encore.
  
  [Authorize]
  [HttpGet ("GetUsers")]
  
  public async Task<ActionResult<IEnumerable<UserResponceDto>>> GetUsers()
  {
    logger.LogInformation("Récupération de la liste de tous les utilisateurs");
    var usersDto = await userService.GetAllAsync();
    return Ok(usersDto);
  }

  [HttpPost("Login")]
  [AllowAnonymous]
  public async Task<ActionResult<LoginResponceDto>> Login([FromBody] LoginRequestDto dto)
  {
    logger.LogInformation("Tentative de cnnexion pour l'utilisateur : {Email}", dto.Email);
    
    Result<LoginResponceDto> result = await userService.LoginAsync(dto);
  
    if (!result.IsSuccess)
    {
      logger.LogWarning("Echec de la connexion pour {Email} : {Reason}", dto.Email, result.ErrorMessage);
      return Unauthorized(result.ErrorMessage);
    }
  
    return Ok(result.Value);
  }
  
  [Authorize]
  [HttpGet("email/{email}")]
  public async Task<ActionResult<UserResponceDto>> GetByEmail(string email)
  {
    var userDto = await userService.GetByEmailAsync(email);

    if (userDto == null)
    {
      logger.LogWarning("Recherche par email : l'utilisateur {Email} n'existe pas", email);
      return NotFound();
    }
      

    return Ok(userDto);
  }

  [HttpPost ("CreateUser")]
  public async Task<ActionResult<UserResponceDto>> CreateUser([FromBody] RegisterRequestDto dto)
  {
    var userDto = await userService.CreateByDtoAsync(dto);
    logger.LogInformation("Nouvel utilisateur créé avec l'id : {UserId} et l'email : {Email}", userDto.Id, userDto.Email);
    return CreatedAtAction(
      nameof(GetByEmail),
      new { email = userDto.Email },
      userDto
    );
  }
  [Authorize]
  [HttpPut("{id}/email")]
  public async Task<ActionResult<UserResponceDto>> UpdateEmail(Guid id, [FromBody] UpdateEmailDto? dto)
  {
    if (dto == null || string.IsNullOrWhiteSpace(dto.Email))
      return BadRequest("L'email ne peut pas être vide.");

    var userDto = await userService.UpdateEmailAsync(id, dto.Email);
    return Ok(userDto);
  }
}