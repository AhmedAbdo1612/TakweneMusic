# TakweneMusic Project Context

This file serves as a comprehensive overview of the `TakweneMusic` codebase state to enable quick context loading for subsequent development sessions.

---

## 1. Project Overview & Tech Stack
`TakweneMusic` is a Clean-Architecture-based track distribution and management system targeting **.NET 10.0** and **PostgreSQL**.

### Chosen Technologies:
*   **Domain & Entities**: Plain C# classes representing the core business models. Includes ASP.NET Core Identity's base user models.
*   **Clean Architecture**: Layered separation of concerns:
    *   `TakweneMusic.Domain` (Core business models & rules, no external dependencies)
    *   `TakweneMusic.Application` (Business logic, MediatR commands/queries, validations, interfaces)
    *   `TakweneMusic.Infrastructure` (Data persistence, EF Core configurations, Identity implementations, JWT validation services)
    *   `TakweneMusic.Api` (Presentation layer, endpoints, Swagger, OpenAPI)
*   **CQRS with MediatR**: Application operations are split cleanly into Commands (mutations) and Queries (reads).
*   **Database & Persistence (EF Core)**: Database access is abstracted via `IApplicationDbContext`. Concrete mapping uses `AppDbContext` pointing to a PostgreSQL server (`Npgsql`).
*   **ASP.NET Core Identity & JWT**: Handled through `IdentityUser<Guid>` for `ApplicationUser` using `UserManager` directly to securely register, authenticate, and manage users. Custom refresh tokens are rotated on each refresh query.
*   **JWT Bearer Authentication**: Registered using `Microsoft.AspNetCore.Authentication.JwtBearer` with validation rules matching configuration keys (`Jwt:Secret`, `Jwt:Issuer`, `Jwt:Audience`).
*   **Carter**: Encapsulates Minimal API endpoints into modular classes (`ICarterModule`) instead of bloating `Program.cs` or relying on Controllers.
*   **NSwag**: Generates OpenAPI/Swagger specifications and UI for Carter Minimal APIs, including configuration for JWT Bearer token authentication in the Swagger UI.
*   **MediatR Pipeline Validation Behavior**: Auto-validates incoming requests implementing `ICommand` using `FluentValidation` before command handlers run.
*   **Global Exception Handling**: Centralized `IExceptionHandler` implementation formats exceptions into standardized RFC 7807 Problem Details.
*   **Standardized Envelope Responses**: A generic `ApiResponse<T>` wrapper formats all successful responses uniformly.

---

## 2. Directory Structure

```text
TakweneMusic/
│
├── TakweneMusic.slnx         # Solution definition
├── appsettings.json          # Configuration including database connection strings
├── context.md                # Project architecture & state summary (this file)
├── README.md                 # Project documentation
├── .gitignore                # Git ignore files configuration (.vs, bin, obj, etc.)
│
└── src/
    ├── TakweneMusic.Domain/
    │   ├── Entities/
    │   │   ├── ApplicationUser.cs   # Custom user inheriting from IdentityUser<Guid>
    │   │   ├── RefreshToken.cs      # User refresh tokens with rotation properties
    │   │   ├── Artist.cs            # Artist details
    │   │   ├── Track.cs             # Music tracks with release info and status
    │   │   ├── Dsp.cs               # Digital Service Providers (e.g. Spotify, Apple Music)
    │   │   └── TrackDistribution.cs # Distribution state of tracks to Dsps
    │   └── Enums/
    │       └── TrackStatus.cs       # Draft, Submitted, Distributed, etc.
    │
    ├── TakweneMusic.Application/
    │   ├── Common/
    │   │   ├── Behaviors/
    │   │   │   └── ValidationBehavior.cs # MediatR behavior running FluentValidation rules
    │   │   ├── Models/
    │   │   │   └── ApiResponse.cs        # Generic ApiResponse<T> envelope and type inference helper
    │   │   └── Interfaces/
    │   │       ├── ICommand.cs              # Custom command markers (ICommand, ICommand<TResponse>)
    │   │       ├── IApplicationDbContext.cs # Database context contract
    │   │       └── IIdentityService.cs       # Authentication & authorization contract
    │   └── Users/
    │       ├── Common/
    │       │   ├── AuthResponseDto.cs       # Standard DTO containing JWT and Refresh Token
    │       │   └── JwtProvider.cs           # Token generation logic (JWT Access & base64 Refresh)
    │       └── Commands/
    │           ├── RegisterUser/            # Register Command & Handler
    │           ├── LoginUser/               # Login Command & Handler
    │           └── RotateRefreshToken/      # Rotation Command & Handler
    │
    ├── TakweneMusic.Infrastructure/
    │   ├── Persistence/
    │   │   ├── AppDbContext.cs             # EF Core Context inheriting from IdentityDbContext
    │   │   ├── Configurations/             # Entity type configurations & seed data
    │   │   └── Repositories/               # Database query implementations
    │   ├── Identity/
    │   │   └── IdentityService.cs          # Implementation of IIdentityService using UserManager
    │   ├── Migrations/                     # Database migrations history (Npgsql-compatible)
    │   └── DependencyInjection.cs          # Registers AppDbContext, IdentityCore, IdentityService, JwtBearer Auth & Authorization
    │
    └── TakweneMusic.Api/
        ├── Endpoints/
        │   ├── Artists/                    # Individual Artist endpoints (GetArtists, CreateArtist, etc.)
        │   ├── Auth/                       # Individual Authentication endpoints (Register, Login, Refresh)
        │   ├── Dsps/                       # Individual DSP endpoints (GetDsps, CreateDsp, etc.)
        │   ├── Tracks/                     # Individual Track endpoints (GetTracks, CreateTrack)
        │   └── TrackDistributions/         # Individual TrackDistribution endpoints (GetTrackDistributions, etc.)
        ├── Middlewares/
        │   └── GlobalExceptionHandler.cs   # Standardized global error mapping to Problem Details
        └── Program.cs                      # Application bootstrapper registering Carter, MediatR, API Explorer, NSwag, and Auth Middlewares
```

---

## 3. Critical Design Decisions & Integrations

1.  **Identity Integration**: Implemented using `IdentityUser<Guid>` to allow UUID primary keys instead of default strings. Redundant properties like `Email`, `Id`, and `PasswordHash` are inherited to prevent mapping conflicts.
2.  **Explicit Timezone Handling (Npgsql 8+)**: PostgreSQL requires `DateTime` values saved to `timestamp with time zone` (`timestamptz`) columns to specify `DateTimeKind.Utc`. All seed data in `TrackConfiguration` and migrations have been wrapped in `DateTime.SpecifyKind(..., DateTimeKind.Utc)` to prevent mapping runtime exceptions.
3.  **Pipeline Request Validation**: Requests entering the MediatR handler system are validated by the global `ValidationBehavior`. If any FluentValidation rules fail, a `ValidationException` is thrown before business execution, returning a `400 Bad Request` with structured field errors.
4.  **CQRS Command/Query Separation**: Commands explicitly implement a custom `ICommand` interface, while Queries implement standard MediatR `IRequest`. `ValidationBehavior` checks if the request is an `ICommand` inside the `Handle` method, ensuring that validation only executes for commands and queries bypass validation entirely.
5.  **Pure Validator Design**: FluentValidation classes strictly validate input structures and formats (e.g. non-emptiness, maximum length, and string patterns) without making database queries. All database state validation checks (e.g., uniqueness constraints and entity existence checks) are executed inside the MediatR Command Handlers, which throw a FluentValidation `ValidationException` on failure.
6.  **Decoupled & Centralized Error Handling**: Try-catch blocks have been eliminated from all Minimal API endpoint definitions. Unhandled validation, bad request, not found, or unauthorized exceptions bubble up to the `GlobalExceptionHandler` which generates standard Problem Details response structures.
7.  **Swagger UI with Bearer Authentication**: Added NSwag to automatically discover Minimal API endpoints registered by Carter, and configured it to show a JWT Authorize button to allow header injection (`Authorization: Bearer <token>`) directly in the Swagger UI.
8.  **Split Endpoint Files**: For a cleaner and modular vertical slice representation, each Minimal API endpoint is isolated into its own class file implementing `ICarterModule` and categorized into entity folders under the `Endpoints/` directory.
9.  **Token Bearer Authentication & Enforced Security**: Added the `Microsoft.AspNetCore.Authentication.JwtBearer` package. Configured and registered JWT authentication and authorization in `TakweneMusic.Infrastructure`. Secured all business endpoints in `Endpoints/` via `.RequireAuthorization()` while keeping the `Auth` endpoints open using `.AllowAnonymous()`.
10. **DI Resolution & App Startup**: Registered `builder.Services.AddEndpointsApiExplorer();` in `Program.cs` to satisfy NSwag discovery requirements, and added the blocking `app.Run();` call to successfully run the web server.
11. **Unified Response Formatting (`ApiResponse<T>`)**: Created a generic wrapper for successful API calls containing `IsSuccess` (bool), `Message` (string), and `Data` (T) properties, alongside a non-generic helper to support compiler type inference. Refactored all 20 Carter endpoints to wrap outputs with this envelope.
12. **Complete OpenAPI Metadata**: Documented all Carter routes with full OpenAPI details using `.WithName()`, `.WithSummary()`, `.WithDescription()`, and explicit `.Produces<T>()` mappings for successful results (`ApiResponse<T>`) and failure scenarios (`ProblemDetails` or `HttpValidationProblemDetails`).

---

## 4. Current State

*   **Authentication & Token Rotation**:
    *   Registration generates custom JWTs and secure base64 refresh tokens.
    *   Logins validate passwords against `UserManager`.
    *   Refreshing access tokens rotates the current refresh token (marks it used and generates a new pair).
*   **Database**: Migrated successfully on PostgreSQL (`TakweenDB`) with seed data for Artists, Dsps, and Tracks.
*   **Compilation & Server execution**:
    *   Builds successfully with **0 warnings** and **0 errors**.
    *   Starts up and runs successfully, listening on `http://localhost:5023`.
*   **Git Repository**: Local repository is fully configured with a `.gitignore` file, committed, and tracked on GitHub under the private repository `AhmedAbdo1612/TakweneMusic`.

---

## 5. Next Steps

1.  **Testing**:
    *   Implement integration and unit tests for authentication, FluentValidation behaviors, and handlers.
2.  **Database Migration Runner**:
    *   Set up a database migration checker at application startup to ensure schema changes auto-apply in Development/Staging.
3.  **Distribution Integration**:
    *   Add background workers (e.g. Quartz or Coravel) to handle async track delivery to mock DSP servers.
