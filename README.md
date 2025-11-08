# 🔧 User Management API

Een moderne ASP.NET Core Web API voor gebruikersbeheer met real-time updates via SignalR.

## 🚀 Features

- ✅ **RESTful API** - Volledige CRUD operaties voor gebruikers
- ✅ **SignalR Hub** - Real-time notificaties bij data wijzigingen
- ✅ **Entity Framework Core** - Database toegang met migrations
- ✅ **SQL Server** - LocalDB voor development
- ✅ **Data Validatie** - Model validatie met Data Annotations
- ✅ **CORS** - Geconfigureerd voor Angular frontend
- ✅ **Swagger UI** - Interactieve API documentatie

## 🛠️ Technologieën

- **ASP.NET Core 9.0**
- **Entity Framework Core 9.0**
- **SQL Server / LocalDB**
- **SignalR**
- **Swagger / OpenAPI**
- **C# 12**

## 📋 Vereisten

- .NET 9.0 SDK
- SQL Server of SQL Server LocalDB
- Visual Studio 2022 / VS Code / Rider (optioneel)

## 🏃‍♂️ Installatie

1. Clone de repository:
```bash
git clone https://github.com/tamer4k/UserManagementAPI.git
cd UserManagementAPI
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Update de database connection string in `appsettings.json` (indien nodig):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UserManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

4. Run database migrations:
```bash
dotnet ef database update
```

5. Start de API:
```bash
dotnet run
```

6. Open Swagger UI: `http://localhost:5151/swagger`

## 📦 Project Structuur

```
UserManagementAPI/
├── Controllers/
│   └── UsersController.cs      # REST API endpoints
├── Data/
│   └── ApplicationDbContext.cs # EF Core DbContext
├── Hubs/
│   └── UserHub.cs              # SignalR Hub
├── Migrations/                  # EF Core migrations
├── Models/
│   └── User.cs                 # User entity met validatie
├── Services/
│   ├── IUserService.cs         # Service interface
│   └── UserService.cs          # Business logic laag
├── Program.cs                  # App configuratie
├── appsettings.json            # Configuratie
└── UserManagementAPI.csproj    # Project file
```

## 🔌 API Endpoints

### Users

| Method | Endpoint | Beschrijving |
|--------|----------|--------------|
| GET | `/api/users` | Haal alle gebruikers op |
| GET | `/api/users/{id}` | Haal specifieke gebruiker op |
| POST | `/api/users` | Voeg nieuwe gebruiker toe |
| PUT | `/api/users/{id}` | Update bestaande gebruiker |
| DELETE | `/api/users/{id}` | Verwijder gebruiker |

### SignalR Hub

- **Hub URL**: `/userHub`
- **Event**: `UserChanged` - Wordt gefired na elke CRUD operatie

## 📝 User Model

```csharp
public class User
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Naam is verplicht")]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is verplicht")]
    [EmailAddress(ErrorMessage = "Ongeldig email adres")]
    [MaxLength(100)]
    public string Email { get; set; }  // Unique
    
    [Required(ErrorMessage = "Wachtwoord is verplicht")]
    [MinLength(6, ErrorMessage = "Wachtwoord moet minimaal 6 karakters zijn")]
    [MaxLength(255)]
    public string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

## 🔐 Validatie Regels

- **Name**: Verplicht, max 100 karakters
- **Email**: Verplicht, uniek, moet geldig email adres zijn
- **Password**: Verplicht, minimaal 6 karakters

## 📡 SignalR Integratie

De API stuurt automatisch `UserChanged` events naar alle verbonden clients wanneer:
- Een nieuwe gebruiker wordt toegevoegd (POST)
- Een gebruiker wordt geüpdatet (PUT)
- Een gebruiker wordt verwijderd (DELETE)

### Voorbeeld SignalR Client (JavaScript/TypeScript)
```typescript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5151/userHub")
    .build();

connection.on("UserChanged", () => {
    console.log("User data changed, reload!");
    // Reload je gebruikerslijst
});

connection.start();
```

## 🌐 CORS Configuratie

CORS is geconfigureerd voor de Angular frontend:
- **Allowed Origins**: `http://localhost:4200`, `http://127.0.0.1:4200`
- **Credentials**: Enabled (voor SignalR)
- **Methods**: Alle
- **Headers**: Alle

## 🗄️ Database

### Migrations

Nieuwe migration maken:
```bash
dotnet ef migrations add MigrationName
```

Database updaten:
```bash
dotnet ef database update
```

Database verwijderen:
```bash
dotnet ef database drop
```

### Seed Data

Om testdata toe te voegen, run SQL queries via SQL Server Management Studio of command line.

## 🧪 Testing met Swagger

1. Start de API
2. Open `http://localhost:5151/swagger`
3. Expand een endpoint
4. Klik op "Try it out"
5. Vul de parameters in
6. Klik op "Execute"

## 🔧 Configuratie

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UserManagementDB;Trusted_Connection=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🤝 Frontend Repository

Deze API werkt samen met de Angular frontend:
- Repository: [user-management-WebUI](https://github.com/tamer4k/user-management-WebUI)
- Technologie: Angular 19, SignalR Client, TypeScript

## 🐛 Troubleshooting

### Database connectie mislukt
- Check of SQL Server LocalDB geïnstalleerd is
- Verify connection string in `appsettings.json`
- Run `dotnet ef database update`

### SignalR werkt niet
- Check CORS instellingen
- Verify dat AllowCredentials() is toegevoegd
- Check browser console voor errors

### Port 5151 is in gebruik
- Wijzig de port in `Properties/launchSettings.json`
- Update ook de frontend environment.ts

## 📚 Dependencies

```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.10" />
<PackageReference Include="Microsoft.AspNetCore.SignalR" Version="1.2.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.6" />
```

## 🔒 Security Notes

⚠️ **Deze API is voor development/educatie doeleinden**

Voor productie moet je:
- ✅ Wachtwoorden hashen (gebruik BCrypt of Identity)
- ✅ Authentication/Authorization implementeren (JWT tokens)
- ✅ HTTPS forceren
- ✅ Rate limiting toevoegen
- ✅ Input validatie versterken
- ✅ Logging en monitoring toevoegen

## 📄 License

Dit project is gemaakt voor educatieve doeleinden.

## 👨‍💻 Auteur

Tamer Al-Ashraf

---

⭐ Vergeet niet om de repository een ster te geven als je het nuttig vindt!

## 📖 Meer Informatie

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [SignalR](https://docs.microsoft.com/aspnet/core/signalr)
