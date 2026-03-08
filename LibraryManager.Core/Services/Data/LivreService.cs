using LibraryManager.Core.Interfaces.Repositories;
using LibraryManager.Core.Interfaces.Services;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;
using System.Linq;
using LibraryManager.Core.DTOs.Requests;
using LibraryManager.Core.DTOs.Requests.LivreRequest;
using LibraryManager.Core.Mappers;

namespace LibraryManager.Core.Services.Data;

public class LivreService(ILivreRepository livreRepository)
  : BaseService<Livre>(livreRepository), ILivreService
{
  #region GetAllAsync

  public override async Task<IEnumerable<Livre>> GetAllAsync()
  {
    var livres = await base.GetAllAsync();
    return livres.OrderBy(l => l.Nom).ToList();
  }

  #endregion

  #region GetLivreDispoAsync

  public async Task<IEnumerable<Livre>> GetLivreDispoAsync()
  {
    var livres = await base.GetAllAsync();
    return livres.Where(l => l.StatutLivre == LivreStatut.Disponible);
  }

  #endregion

  #region ChangeStatutAsync

  public async Task<Livre?> ChangeStatutAsync(Guid livreId, LivreStatut nouveauStatut)
  {
    var livre = await base.GetByIdAsync(livreId);
  
    if (livre == null)
      throw new Exception("Livre introuvable");
  
    if (livre.StatutLivre == nouveauStatut)
      return livre;
  
    livre.StatutLivre = nouveauStatut;
  
    await base.UpdateAsync(livre);
  
    return livre;
  }

  #endregion

  #region CreateFromDtoAsync

  public async Task<Livre> CreateFromDtoAsync(LivreRequestDTO dto)
  {
    if (string.IsNullOrWhiteSpace(dto.Nom))
    {
      throw new ArgumentException("Pas d'espace pour nom");
    }

    if (string.IsNullOrWhiteSpace(dto.Auteur))
    {
      throw new ArgumentException("Pas d'espace pour auteur");
    }

    Livre livres = dto.ToEntity();
    //livres.StatutLivre = LivreStatut.Disponible;
    return await this.AddAsync(livres);
  }

  #endregion

  #region CreateAsync

  public override async Task<Livre> AddAsync(Livre livre)
  {
    if (string.IsNullOrWhiteSpace(livre.Nom) || string.IsNullOrWhiteSpace(livre.Auteur))
    {
      throw new ArgumentException("Le nom et l'auteur sont obligatoires pour référencer un livre.");
    }

    //livre.StatutLivre = LivreStatut.Disponible;

    return await base.AddAsync(livre);
  }

  #endregion

  #region UpdateAsync

  public async Task UpdateLivreAsync(Guid id, Livre livre)
  {
    var livreExist = await base.GetByIdAsync(id);
    if (livreExist == null) throw new Exception("Livre introuvable");

    livreExist.Nom = livre.Nom;
    livreExist.Auteur = livre.Auteur;
    livreExist.NbPages = livre.NbPages;
    livreExist.DateDeSortie = livre.DateDeSortie;
    livreExist.StatutLivre = livre.StatutLivre;
    livreExist.DateModification = livre.DateModification;
    await base.UpdateAsync(livreExist);
  }

  #endregion

  
}