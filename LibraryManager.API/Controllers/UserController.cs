using LibraryManager.Core.Common;
using LibraryManager.Core.DTOs.Requests.UserRequest;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet ("GetUsers")]
  [Authorize]
  public async Task<ActionResult<IEnumerable<UserResponceDto>>> GetUsers()
  {
    var usersDto = await _userService.GetAllAsync();
    return Ok(usersDto);
  }

  [HttpPost("Login")]
  public async Task<ActionResult<LoginResponceDto>> Login([FromBody] LoginRequestDto dto)
  {
    Result<LoginResponceDto> result = await _userService.LoginAsync(dto);
  
    if (!result.IsSuccess)
    {
      return Unauthorized(result.ErrorMessage);
    }
  
    return Ok(result.Value);
  }

  [HttpGet("email/{email}")]
  public async Task<ActionResult<UserResponceDto>> GetByEmail(string email)
  {
    var userDto = await _userService.GetByEmailAsync(email);
    if (userDto == null)
      return NotFound();

    return Ok(userDto);
  }

  [HttpPost ("CreateUser")]
  public async Task<ActionResult<UserResponceDto>> CreateUser([FromBody] RegisterRequestDto dto)
  {
    var userDto = await _userService.CreateByDtoAsync(dto);
    return CreatedAtAction(
      nameof(GetByEmail),
      new { email = userDto.Email },
      userDto
    );
  }

  [HttpPut("{id}/email")]
  public async Task<ActionResult<UserResponceDto>> UpdateEmail(Guid id, [FromBody] UpdateEmailDto? dto)
  {
    if (dto == null || string.IsNullOrWhiteSpace(dto.Email))
      return BadRequest("L'email ne peut pas être vide.");

    var userDto = await _userService.UpdateEmailAsync(id, dto.Email);
    return Ok(userDto);
  }
}