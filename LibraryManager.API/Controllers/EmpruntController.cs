using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.EmpruntRequestDto;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Mappers;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class EmpruntController(IEmpruntService empruntService) : ControllerBase
  {
    [HttpGet ("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
      return Ok(await empruntService.GetAllAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      return Ok(await empruntService.GetByIdAsync(id));
    }

    [HttpPost ("Emprunter")]
    public async Task<IActionResult> EmprunterLivreAsync([FromBody] EmpruntRequestDto dto)
    {
      var emprunt = await empruntService.EmprunterLivreAsync(dto.LivreId, dto.UserId);
      return Ok(emprunt);
    }

    [HttpPut("{id}/retour")]
    public async Task<IActionResult> Retourner(Guid id)
    {
      var emprunt = await empruntService.RendreLivreAsync(id);
      return Ok(emprunt);
    }
  }
}