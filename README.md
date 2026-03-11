# 📚 LibraryManager API

LibraryManager est une API de gestion de bibliothèque développée en **.NET 10.0**. Le projet suit les principes de la **Clean Architecture** pour garantir une séparation claire des responsabilités, une maintenance facilitée et une scalabilité optimale.

## 🏗️ Architecture du Projet

Le projet est découpé en 4 couches principales :

1.  **LibraryManager.API** : Couche de Présentation (Controllers, Middlewares, Configurations API).
2.  **LibraryManager.Core** : Couche Application (Interfaces, Services, DTOs, Mappers, Logique métier).
3.  **LibraryManager.Infrastructure** : Couche Data (DbContext, Repositories, Configurations EF Core).
4.  **LibraryManager.Domain** : Couche Domaine (Entités de base, Enums).

## 🚀 Technologies et Bibliothèques

* **Framework** : .NET 10.0
* **ORM** : Entity Framework Core
* **Base de Données** : SQL Server
* **Sécurité** : 
    * Authentification JWT (JSON Web Token)
    * Hachage de mots de passe avec BCrypt
* **Documentation** : Scalar (alternative moderne à Swagger)
* **Pattern** : Repository Pattern & Dependency Injection

## 🛠️ Installation et Lancement

1.  **Clonage du dépôt** :
    ```bash
    git clone <url-du-repo>
    cd Web-Api_CleanArchi_LibraryManager
    ```

2.  **Configuration de la base de données** :
    Modifiez la chaîne de connexion `DefaultConnection` dans le fichier `LibraryManager.API/appsettings.json`.

3.  **Appliquer les migrations** :
    ```bash
    dotnet ef database update --project LibraryManager.Infrastructure --startup-project LibraryManager.API
    ```

4.  **Exécuter l'API** :
    ```bash
    dotnet run --project LibraryManager.API
    ```

## 🛣️ Liste des Endpoints (API)

### 👤 Gestion des Utilisateurs (`/api/User`)
| Méthode | Point de terminaison | Description | Accès |
| :--- | :--- | :--- | :--- |
| **POST** | `/Register` | Inscription d'un nouvel utilisateur | Public |
| **POST** | `/Login` | Connexion et récupération du Token JWT | Public |
| **GET** | `/GetAll` | Liste tous les utilisateurs | Admin |
| **GET** | `/GetByEmail` | Récupère les détails d'un utilisateur par email | Authentifié |
| **PUT** | `/UpdateEmail` | Met à jour l'email de l'utilisateur connecté | Authentifié |
| **DELETE** | `/Delete` | Supprime un compte utilisateur | Admin / Propriétaire |

### 📖 Gestion des Livres (`/api/Livre`)
| Méthode | Point de terminaison | Description | Accès |
| :--- | :--- | :--- | :--- |
| **GET** | `/GetAll` | Récupère la liste de tous les livres | Public |
| **GET** | `/{id}` | Récupère un livre par son identifiant | Public |
| **POST** | `/Create` | Ajoute un nouveau livre au catalogue | Admin |
| **PUT** | `/Update/{id}` | Modifie les informations d'un livre | Admin |
| **DELETE** | `/Delete/{id}` | Supprime un livre | Admin |

### 📑 Gestion des Emprunts (`/api/Emprunt`)
| Méthode | Point de terminaison | Description | Accès |
| :--- | :--- | :--- | :--- |
| **GET** | `/GetAll` | Liste tous les emprunts enregistrés | Admin |
| **POST** | `/Create` | Enregistre un nouvel emprunt (met à jour le statut du livre) | Authentifié |
| **PUT** | `/Return/{id}` | Enregistre le retour d'un livre | Authentifié |

## 🛡️ Fonctionnalités Clés

* **Gestion des Erreurs** : Un middleware global (`ExceptionMiddleware`) capture les erreurs et renvoie un format JSON standardisé.
* **Validation Métier** : Utilisation d'une classe `Result<T>` pour gérer les succès et les échecs de manière explicite sans lever d'exceptions inutiles.
* **Mapping** : Transformation manuelle et efficace des entités vers les DTOs via des classes de mapping dédiées.
* **Sécurité** : Protection des routes sensibles via des politiques d'autorisation basées sur les rôles (`Admin`, `User`).

* Ce projet contient tous les secrets et string de connection etc mais rien n'est en ligne tout est local donc je me suis permis.

## 📄 Licence
Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
