using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.EmpruntRequestDto;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Core.Mappers;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmpruntController : ControllerBase
  {
    private readonly IEmpruntService _empruntService;


    public EmpruntController(IEmpruntService empruntService)
    {
      _empruntService = empruntService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
      return Ok(await _empruntService.GetAllAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      return Ok(await _empruntService.GetByIdAsync(id));
    }

    [HttpPost]
    [Route("api/[controller]")]
    public async Task<IActionResult> EmprunterLivreAsync([FromBody] EmpruntRequestDto dto)
    {
      var emprunt = await _empruntService.EmprunterLivreAsync(dto.LivreId, dto.UserId);
      return Ok(emprunt);
    }

    [HttpPut("{id}/retour")]
    public async Task<IActionResult> Retourner(Guid id)
    {
      var emprunt = await _empruntService.RendreLivreAsync(id);
      return Ok(emprunt);
    }
  }
}