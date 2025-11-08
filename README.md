# User Management API

Een C# Web API project met Swagger voor het beheren van gebruikers met CRUD operaties.

## Technologieën

- **ASP.NET Core 9.0** - Web API Framework
- **Entity Framework Core 9.0** - ORM voor database toegang
- **SQL Server LocalDB** - Database
- **Swagger/OpenAPI** - API documentatie
- **Dependency Injection** - Service architecture

## Project Structuur

```
UserManagementAPI/
├── Controllers/
│   └── UsersController.cs      # API endpoints voor user management
├── Models/
│   └── User.cs                 # User entiteit
├── Services/
│   ├── IUserService.cs         # Service interface
│   └── UserService.cs          # Service implementatie
├── Data/
│   └── ApplicationDbContext.cs # Database context
├── Migrations/                 # Entity Framework migraties
└── Program.cs                  # Application entry point
```

## Database Schema

### Users Tabel
- **Id** (int, Primary Key, Auto-increment)
- **Name** (string, max 100 characters, verplicht)
- **Email** (string, max 100 characters, uniek, verplicht)
- **Password** (string, max 255 characters, verplicht)
- **CreatedAt** (DateTime, automatisch ingesteld)
- **UpdatedAt** (DateTime?, nullable)

## API Endpoints

### GET /api/users
Haal alle gebruikers op
- **Response**: 200 OK met lijst van users

### GET /api/users/{id}
Haal een specifieke gebruiker op
- **Response**: 200 OK met user data of 404 Not Found

### POST /api/users
Maak een nieuwe gebruiker aan
- **Request Body**: User object (zonder Id)
- **Response**: 201 Created met de nieuwe user

### PUT /api/users/{id}
Update een bestaande gebruiker
- **Request Body**: User object
- **Response**: 200 OK met updated user of 404 Not Found

### DELETE /api/users/{id}
Verwijder een gebruiker
- **Response**: 204 No Content of 404 Not Found

## Project Starten

### 1. Navigeer naar de project directory
```bash
cd "C:\Users\tamer\OneDrive\Desktop\C#\UserManagementAPI"
```

### 2. Start het project
```bash
dotnet run
```

### 3. Open Swagger UI
Navigeer naar een van de volgende URLs in je browser:
- **HTTPS**: https://localhost:5001/swagger
- **HTTP**: http://localhost:5000/swagger

## Database Management

### Nieuwe migratie toevoegen
```bash
dotnet ef migrations add NaamVanMigratie
```

### Database updaten
```bash
dotnet ef database update
```

### Migratie verwijderen
```bash
dotnet ef migrations remove
```

### Database verwijderen
```bash
dotnet ef database drop
```

## Voorbeeld Gebruik

### User aanmaken (POST)
```json
{
  "name": "Jan de Vries",
  "email": "jan@example.com",
  "password": "GeheimWachtwoord123"
}
```

### User updaten (PUT)
```json
{
  "name": "Jan de Vries (Updated)",
  "email": "jan.updated@example.com",
  "password": "NieuwWachtwoord456"
}
```

## Configuratie

De database connection string staat in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UserManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

## Ontwikkeling

### Packages
Alle benodigde NuGet packages zijn reeds geïnstalleerd:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
- Swashbuckle.AspNetCore

### Build het project
```bash
dotnet build
```

### Run tests (indien aanwezig)
```bash
dotnet test
```

## Opmerkingen

- Wachtwoorden worden momenteel in plain text opgeslagen. Voor productie gebruik zou je password hashing moeten implementeren (bijv. met BCrypt of Identity Framework).
- De API heeft momenteel geen authenticatie/autorisatie. Overweeg het toevoegen van JWT tokens of ASP.NET Core Identity voor productie.
- CORS is niet geconfigureerd. Voeg dit toe als je de API wilt gebruiken vanuit een frontend applicatie.

## Licentie

Dit is een demo/leer project.

