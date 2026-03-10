📚 LibraryManager API

LibraryManager est une solution de gestion de bibliothèque moderne développée en .NET 10.0. Le projet suit les principes de la Clean Architecture pour garantir une séparation claire des responsabilités, une testabilité accrue et une maintenance facilitée.
🏗️ Architecture du Projet

Le projet est structuré en quatre couches principales :

    LibraryManager.API : La couche de présentation (ASP.NET Core Web API). Elle contient les contrôleurs, les middlewares et la configuration de l'application.

    LibraryManager.Core : La couche de logique métier (Application). Elle définit les interfaces, les services, les DTOs (Data Transfer Objects) et les mappers.

    LibraryManager.Domain : La couche de domaine. Elle contient les entités de base, les enums et la logique métier fondamentale.

    LibraryManager.Infrastructure : La couche d'accès aux données et de services externes. Elle contient le contexte Entity Framework, les implémentations des répertoires et les configurations de base de données.

🚀 Technologies Utilisées

    Framework : .NET 10.0

    ORM : Entity Framework Core

    Base de Données : SQL Server (configuré via LibraryManagerContext)

    Sécurité : JWT (JSON Web Tokens) pour l'authentification et BCrypt pour le hachage des mots de passe

    Documentation : Scalar / Swagger

🛠️ Installation et Configuration
Prérequis

    SDK .NET 10.0

    SQL Server

Étapes

    Cloner le dépôt :
    Bash

    git clone https://github.com/votre-repo/LibraryManager.git
    cd LibraryManager

    Configurer la base de données :
    Mettez à jour la chaîne de connexion dans le fichier appsettings.json du projet LibraryManager.API.

    Appliquer les migrations :
    Bash

    dotnet ef database update --project LibraryManager.Infrastructure --startup-project LibraryManager.API

    Lancer l'application :
    Bash

    dotnet run --project LibraryManager.API

🛣️ Points de Terminaison (Endpoints) API

L'API est organisée autour de trois contrôleurs principaux :
👤 Utilisateurs (/api/User)
Méthode	Endpoint	Description	Accès
POST	/Register	Créer un nouveau compte utilisateur	Public
POST	/Login	Authentification et génération de token JWT	Public
GET	/GetAll	Liste de tous les utilisateurs	Admin
GET	/GetByEmail	Récupérer un utilisateur par son email	Authentifié
PUT	/UpdateEmail	Modifier l'adresse email	Authentifié
DELETE	/Delete	Supprimer un compte	Admin / Soi-même
📖 Livres (/api/Livre)
Méthode	Endpoint	Description	Accès
GET	/GetAll	Liste de tous les livres	Public
GET	/{id}	Détails d'un livre spécifique	Public
POST	/Create	Ajouter un nouveau livre	Admin
PUT	/Update/{id}	Modifier les informations d'un livre	Admin
DELETE	/Delete/{id}	Supprimer un livre	Admin
📑 Emprunts (/api/Emprunt)
Méthode	Endpoint	Description	Accès
GET	/GetAll	Historique de tous les emprunts	Admin
POST	/Create	Enregistrer un nouvel emprunt	Authentifié
PUT	/Return/{id}	Marquer un livre comme retourné	Authentifié
🔒 Sécurité et Rôles

L'application utilise des rôles pour restreindre l'accès à certaines fonctionnalités :

    User : Peut consulter les livres et gérer ses propres emprunts.

    Admin : Possède tous les droits, y compris la gestion du catalogue de livres et des utilisateurs.

📁 Structure des fichiers clés

    ExceptionMiddleware.cs : Gestion globale des erreurs pour des réponses API cohérentes.

    JwtProvider.cs : Logique de création des tokens de sécurité.

    LibraryManagerContext.cs : Configuration Fluent API pour le mapping SQL.

    Result.cs : Objet générique pour encapsuler les réponses des services (Succès/Échec).
