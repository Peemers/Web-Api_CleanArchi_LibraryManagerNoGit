using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.LivreRequest;
using LibraryManager.Core.DTOs.Responces;
using LibraryManager.Core.DTOs.Responces.LivreResponse;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Mappers;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivreController : ControllerBase
{
  private ILivreService? _livreService;

  public LivreController(ILivreService livreService)
  {
    _livreService = livreService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<LivreResponceDto>>> GetLivres()
  {
    var livres = await _livreService.GetAllAsync();
    return Ok(livres);
  }

  [HttpPost]
  public async Task<IActionResult> PostLivre(LivreRequestDTO dto)
  {
    // appelle de la méthode que l'on a créée spécifiquement pour les DTOs
    var livreCree = await _livreService.CreateFromDtoAsync(dto);

    // On renvoie un DTO de réponse, pas l'entité brute
    return CreatedAtAction(
      nameof(GetById),
      new {id = livreCree.Id},
      livreCree.ToResponseDto()
    );
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    var livre = await _livreService.GetByIdAsync(id);

    if (livre == null)
      return NotFound();

    return Ok(livre);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Livre livre)
  {
    if (id != livre.Id) return BadRequest("L'ID ne correspond pas");
    await _livreService.UpdateAsync(livre);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAsync(Guid id)
  {
    var livre = await _livreService.GetByIdAsync(id);
    if (livre == null) return NotFound("Livre pas trouvé");

    await _livreService.DeleteAsync(id);
    return NoContent();
  }
}