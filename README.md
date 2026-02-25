LibraryManager est une solution de gestion de bibliothèque moderne développée en C# / .NET 10.0. Le projet a été conçu avec une attention particulière portée à la Clean Architecture (ou Architecture Hexagonale), garantissant que la logique métier reste isolée des frameworks, des bases de données et des interfaces externes.
🏗️ Architecture de la Solution

La solution est découpée en quatre projets distincts, chacun ayant une responsabilité précise :
1. LibraryManager.Domain (Le Noyau)

C'est la couche la plus interne. Elle contient les objets métier fondamentaux sans aucune dépendance technique.

    Entities : Définition des modèles de données pivots comme Livre, User (Adhérent) et Emprunt.

    Enums : États de gestion tels que LivreStatut (Disponible, Emprunté) et UsersRoles.

2. LibraryManager.Core (Logique Applicative)

Cette couche orchestre les flux de données et contient les règles métier.

    Interfaces : Définition des contrats pour les services (ILivreService) et les dépôts (ILivreRepository).

    Services : Implémentations de la logique (ex: validation d'un emprunt, calcul de dates).

    DTOs & Mappers : Objets de transfert pour l'API (Requests/Responses) et classes de conversion pour isoler les entités du domaine.

3. LibraryManager.Infrastructure (Détails Techniques)

Cette couche gère la communication avec les outils externes.

    Data Access : Utilisation d'Entity Framework Core pour la persistance.

    Repositories : Implémentations concrètes des accès aux données utilisant le LibraryManagerContext.

    Configuration : Mapping détaillé via Fluent API (LivreConfiguration, etc.) pour définir les contraintes SQL.

4. LibraryManager.API (Point d'Entrée)

La couche de présentation exposant les fonctionnalités via une interface RESTful.

    Controllers : Points d'accès pour les livres et les utilisateurs.

    Middleware : Gestion centralisée des exceptions via ExceptionMiddleware.

    Configuration : Enregistrement des services et pipeline HTTP dans le Program.cs.

🛠️ Stack Technique

    Framework : .NET 10.0

    Base de données : SQL Server (via EF Core)

    Documentation : OpenAPI (Swagger) avec interface Scalar

    Outils : Injection de dépendances native, Fluent API

⚙️ Installation et Configuration
Prérequis

    .NET 10 SDK

    SQL Server

Configuration de la base de données

    Ouvrez LibraryManager.API/appsettings.json.

    Modifiez la chaîne de connexion Default selon votre instance SQL.

Lancement
Bash

# Restaurer les packages NuGet
dotnet restore

# Lancer l'API
dotnet run --project LibraryManager.API

🚀 Fonctionnalités Implémentées & WIP
Terminées ✅

    Architecture multi-projets découplée.

    Système d'injection de dépendances pour tous les services.

    Modèles de données pour Livres, Utilisateurs et Emprunts.

    Gestion globale des erreurs API.

    Documentation interactive via Scalar (disponible en mode développement).

En cours de développement (WIP) ⏳

    Finalisation de la logique de validation des emprunts.

    Mise en place complète des migrations de base de données.

    Tests unitaires avec xUnit et Moq.

🔗 Endpoints Principaux

    Livres : GET /api/Livre (Liste), POST /api/Livre (Ajout).

    Utilisateurs : POST /api/User/register (Inscription), POST /api/User/login (Connexion).

    Santé : GET /test (Vérification de l'API).

🤝 Contribution

Le projet utilise les principes de la Clean Architecture. Toute contribution doit respecter la séparation des couches : ne jamais référencer l'Infrastructure dans le Domain ou le Core.
