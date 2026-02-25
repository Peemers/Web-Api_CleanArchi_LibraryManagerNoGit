using LibraryManager.Core.DTOs.Responces.EmpruntResponseDto;
using LibraryManager.Core.DTOs.Responces.UserResponse;
using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;
using LibraryManager.Core.Mappers;

namespace LibraryManager.Core.Services.Data;

public class EmpruntService(
  IEmpruntRepository empruntRepository,
  ILivreRepository livreRepository)
  : BaseService<Emprunt>(empruntRepository), IEmpruntService
{
  public async Task<EmpruntResponseDto> EmprunterLivreAsync(Guid livreId, Guid userId)
  {
    var livre = await livreRepository.GetByIdAsync(livreId);
    if (livre == null) throw new KeyNotFoundException("Livre introuvable");


    if (livre.StatutLivre != LivreStatut.Disponible)
      throw new ArgumentException("Ce livre n'est pas disponible.");


    livre.StatutLivre = LivreStatut.NonDisponible;
    await livreRepository.UpdateAsync(livre);


    var nouvelEmprunt = new Emprunt
    {
      LivreId = livreId,
      UserId = userId,
      DateEmprunt = DateTime.UtcNow,
      DateRetourPrevu = DateTime.UtcNow.AddDays(14),
    };

    await empruntRepository.AddAsync(nouvelEmprunt);

    return nouvelEmprunt.ToDto();
  }

  public async Task<EmpruntResponseDto> RendreLivreAsync(Guid empruntId)
  {
    //recup de l'emprunt
    var emprunt = await empruntRepository.GetByIdAsync(empruntId);
    if (emprunt == null) throw new KeyNotFoundException("Emprunt introuvable");
    if (emprunt.DateRetourEffective != null) throw new ArgumentException("Ce livre à deja été rendu");

    //recup du livre
    var livre = await livreRepository.GetByIdAsync(emprunt.LivreId);
    if (livre == null) throw new KeyNotFoundException("Livre associé à l'emprunt introuvable");

    //maj emprunt et statut
    emprunt.DateRetourEffective = DateTime.UtcNow;
    livre.StatutLivre = LivreStatut.Disponible;

    //sauvegarde sur db

    await livreRepository.UpdateAsync(livre);
    await empruntRepository.UpdateAsync(emprunt);
    return emprunt.ToDto();
  }
}