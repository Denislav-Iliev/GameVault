# GameVault

GameVault is an ASP.NET Core MVC web application developed as a course project for the SoftUni ASP.NET Fundamentals course.

The application allows users to browse a game catalog, maintain a personal game library, track their playing time, and share reviews. Administrators can manage the available games.

## Technologies Used

- C# and ASP.NET Core MVC
- Entity Framework Core (Code First)
- Microsoft SQL Server
- ASP.NET Core Identity
- Razor Views
- Bootstrap
- HTML and CSS
- Dependency Injection

## Main Features

### Guest Users
- Browse the game catalog.
- View game details and public reviews.
- Register a new account or log in.

### Registered Users
- Add games to their personal library.
- Track and update hours played.
- Remove games from their library.
- Write reviews and ratings.
- Edit and delete their own reviews.
- Access their account profile.

### Administrators
- Add new games to the catalog.
- Edit existing games.
- Delete games.
- Access administrator-only game management actions.

## Project Structure

The solution is separated into multiple projects:

- **GameVault** – ASP.NET Core MVC application, controllers and views.
- **GameVault.Data** – database context and database configuration.
- **GameVault.Data.Models** – entity models.
- **GameVault.GCommon** – shared constants.
- **GameVault.Services.Core** – application services and business logic.
- **GameVault.Services.Core.Models** – data transfer models.

The application uses services and dependency injection to separate business logic from controllers.

## Database

The application uses Microsoft SQL Server and Entity Framework Core with a Code First approach.

The main entities include:

- Game
- Genre
- ApplicationUser
- UserGame
- Review

`UserGame` connects users with games in their personal libraries, while `Review` stores user reviews for individual games.

## Local Development Configuration

The application uses a local Microsoft SQL Server database.

The default connection string is configured in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=GameVault;Integrated Security=True;Encrypt=False"
  }
}
```

The default configuration expects a local SQL Server instance accessible through `Server=.` using Windows Authentication.

The application should run in the **Development** environment for local testing and automatic creation of the demonstration administrator account.

No external API keys are required for the main game catalog, library, and review features.

## How to Run the Project

1. Clone or download the repository.
2. Open `GameVault.slnx` in Visual Studio.
3. Make sure Microsoft SQL Server is installed and running.
4. Restore the NuGet packages.
5. Apply the existing Entity Framework Core migrations to create the database.
6. Set the **GameVault** web project as the startup project.
7. Run the application.

The database connection can be adjusted in `appsettings.Development.json` if a different SQL Server instance is used.

## Demo Administrator Account

For local testing, the application automatically creates a demonstration administrator account when running in the Development environment.

**Email:** `admin@gamevault.local`  
**Password:** `GameVaultAdmin123!`

These credentials are intended for local demonstration purposes only and should not be used in a production environment.

Regular users can create their own accounts through the registration page.

## Authorization

GameVault uses ASP.NET Core Identity for authentication and role-based authorization.

Only administrators can create, edit, and delete catalog games.

Registered users can manage their personal libraries and reviews. Users cannot modify reviews or library records belonging to other accounts.

## Author

## Author

**Denislav Iliev**  
SoftUni – ASP.NET Fundamentals  
October 2026

Developed as a SoftUni ASP.NET Fundamentals course project.