using System.Net;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.Json;

namespace LibraryManager.API.Middleware;

//comme un tuyau, le programme entre d'un coté, ressort de l'autre avec au milieu les middleware

//ici si je capte bien on "scan" les requetes par cette methode
//try on laisse passer la requete, catch on l'attrape au vol 
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>  logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Une erreur est survenue."); //inscription dans le logger.
      await HandleExceptionAsync(context, ex);
    }
  } 
//Si erreur il y a, cette methode s'en charge.
  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json"; //le navigateur est prevenu qu'on va envoyer du json
    
    //traduction de l'erreur c# en HTTP
    context.Response.StatusCode = exception switch
    {
      ArgumentException => (int)HttpStatusCode.BadRequest, //pour les throws, erreur 400 (bad request)
      _ => (int)HttpStatusCode.InternalServerError //Pour les erreurs inconnues, erreur 500(erreur serveur)
    };
    
    //creation d'un objet avec les infos d'erreur
    var response = new
    {
      StatusCodes = context.Response.StatusCode,
      Message = exception.Message, // le message du throw ou de l'erreur _
      Detailed = exception.StackTrace // le chemin de l'erreur
    };
    return context.Response.WriteAsync(JsonSerializer.Serialize(response));
  }
}