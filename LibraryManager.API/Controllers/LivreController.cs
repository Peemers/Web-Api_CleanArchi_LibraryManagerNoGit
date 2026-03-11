using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.LivreRequest;
using LibraryManager.Core.DTOs.Responces;
using LibraryManager.Core.DTOs.Responces.LivreResponse;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Mappers;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LivreController (ILivreService livreService, ILogger<LivreController> logger) : ControllerBase //prim ctor
{
  //private ILivreService? _livreService;

  // public LivreController(ILivreService livreService)
  // {
  //   _livreService = livreService;
  // } Aussi commenté pour le prim ctor

  [HttpGet("GetAll")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<LivreResponceDto>>> GetLivres()
  {
    var livres = await livreService.GetAllAsync();
    return Ok(livres);
  }

  [HttpPost("PostLivre")]
  [Authorize(Roles = "Admin")]
  public async Task<ActionResult<LivreResponceDto>> PostLivre([FromBody] LivreRequestDTO dto)
  {
    var livreCree = await livreService.CreateFromDtoAsync(dto);
    
    logger.LogInformation("Livre {nom} cree avec succès (ID:{Id})", livreCree.Nom, livreCree.Id);
    return CreatedAtAction(
      nameof(GetById),
      new { id = livreCree.Nom },
      livreCree.ToResponseDto()
      //CreatedAtAction (201)created, renvoi l'url de l'objet cree dans le header
      );
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IActionResult> GetById(Guid id)
  {
    var livre = await livreService.GetByIdAsync(id);

    if (livre == null)
      return NotFound();

    return Ok(livre);
  }

  [HttpPut("{id}")]
  [Authorize(Roles =  "Admin")]
  public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] LivreRequestDTO dto)
  {
    logger.LogWarning("Récupération de la liste de livres existants");
    Livre? livreExist = await livreService.GetByIdAsync(id);
    if (livreExist == null)
    {
      logger.LogWarning($"Tentative de modification du livre : {id} par, Résultat : non trouvé");
      return NotFound("Livre introuvable");
    }
    
    livreExist.Nom =  dto.Nom;
    livreExist.Auteur = dto.Auteur;
    livreExist.NbPages = dto.NbPages;
    livreExist.DateDeSortie = dto.DateDeSortie;
    
    await livreService.UpdateAsync(livreExist);
    logger.LogWarning($"Modification du livre {id} reussie");
    return NoContent();
  }

  [HttpDelete("{id}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteAsync(Guid id)
  {
    var livre = await livreService.GetByIdAsync(id);
    if (livre == null) return NotFound("Livre pas trouvé");

    await livreService.DeleteAsync(id);
    return NoContent();
  }
}