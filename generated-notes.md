🚀 TakweneMusic Development - Prompts & Engineering Decisions
This file documents all the prompts used with AI to build the TakweneMusic project using Clean Architecture and the CQRS pattern, along with manual modifications made to ensure code quality and system security.

🛠️ Phase 1: CLI Session Tracking
📌 Prompt: Launching the tool with session logging enabled
DOS
agy --log-file=ai-session.txt
🟦 Phase 2: Domain Layer
📌 Prompt: Generating Enums and Entities in a single line
Plaintext
Create the C# domain entities and enums inside the TakweneMusic.Domain project, including Artist, Track (with a unique Isrc string), Dsp, TrackDistribution, and matching enums for TrackStatus (Draft, Submitted, Distributed) and DistributionStatus (Pending, Live, Rejected) using clean C# primitive types and private setters.
⚠️ Manual Modification & Engineering Decision:
Issue: The AI might introduce external dependencies or infrastructure leakage inside the Domain layer.

Correction: Reviewed the Domain project to ensure it remains 100% pure and completely free of any EF Core references or external NuGet packages (using Microsoft.EntityFrameworkCore;), adhering strictly to Clean Architecture rules.

🟩 Phase 3: Infrastructure & DB Schema
📌 Prompt: Creating the DbContext and configuring relationships
Plaintext
Create an EF Core AppDbContext inside the TakweneMusic.Infrastructure project, implementing DbSets for Artist, Track, Dsp, and TrackDistribution, and configure the schema via Fluent API to enforce a unique index on Track.Isrc and establish foreign key relationships according to Clean Architecture rules.
🛑 Manual Modification & Engineering Decision:
Issue: The AI initially configured a destructive DeleteBehavior.Cascade between the Track and TrackDistribution tables. This would mean deleting a track automatically purges all its distribution history.

Correction: Modified the entity and Fluent API mapping to make TrackId a nullable foreign key (Guid?) and changed the delete behavior to DeleteBehavior.SetNull.

Reason: This ensures that even if a track record is purged, the historical distribution auditing metrics remain intact in the database for financial and analytics reporting.

🗂️ Phase 4: Data Seeding Separation
📌 Prompt: Separating seed data from the main DbContext class
Plaintext
Refactor the AppDbContext in TakweneMusic.Infrastructure to separate the seed data from the main context class by creating a dedicated seed data extension method or implementing IEntityTypeConfiguration classes for the entities to keep the DbContext clean.
🟨 Phase 5: Application Layer
📌 Prompt: Building Use Cases, DTOs, and FluentValidation
Plaintext
Inside the TakweneMusic.Application project, generate the complete MediatR structure for creating a track (CreateTrackCommand) and filtering tracks (GetTracksQuery), including their respective Handlers, TrackDto, and FluentValidation validators to enforce mandatory fields and correct ISRC formats.
🔒 Manual Modification & Engineering Decision:
Issue: In CreateTrackCommandHandler, the AI attempted to save the track directly without checking if the provided ArtistId exists in the database, relying heavily on database-level constraints to throw an unhandled exception.

Correction: Added an explicit validation check within the MediatR handler to verify the artist's existence first, returning a clean 400 Bad Request or custom business exception instead of allowing a database crash to result in an unhandled 500 Internal Server Error.


Markdown
# 🛠️ TakweneMusic Architecture & Refactoring History

## 1. Architectural Issue: Service Lifetime Dependency Conflict

### ❌ Original Error
`Cannot consume scoped service 'IApplicationDbContext' from singleton 'FluentValidation.IValidator'`
* **Cause:** FluentValidation validators were registered as `Singleton` components by default, yet they injected `IApplicationDbContext`, which operates under a `Scoped` lifetime. ASP.NET Core dependency injection patterns strictly forbid capturing short-lived scoped services within long-lived singletons to prevent concurrency anomalies and memory leaks.

### 🛠️ Applied Resolution
The service registration within the Application layer’s `DependencyInjection.cs` file was configured to explicitly enforce a `Scoped` lifetime:
```csharp
services.AddValidatorsFromAssembly(assembly, ServiceLifetime.Scoped);
2. Pipeline Behavior Instantiation Bottleneck
❌ Secondary Error
Cannot instantiate implementation type 'ValidationBehavior2' for service type 'MediatR.IPipelineBehavior2'

Cause: The open generic registration of ValidationBehavior<TRequest, TResponse> lacked proper type constraints, causing structural registration drops when third-party components (such as Carter's default endpoint lookup) implicitly overrode specific registration scopes back to Singleton.

🛠️ Applied Resolution
The application design migrated to a "Thin Validators, Rich Handlers" architectural pattern. Relational validation steps requiring a database context were separated out from FluentValidation and reallocated directly into their matching MediatR Handlers.

3. Automated Refactoring Prompts Registry
📑 1. Request / Command Contract Standardization Prompt
Plaintext
Refactor all Command records/classes within the TakweneMusic.Application project to explicitly implement the MediatR IRequest interface (or your custom ICommand if it inherits from IRequest), and ensure that every validator class targets only these Commands while keeping Queries unaffected.
🧽 2. Decoupled Business Logic Isolation (Thin Validators) Prompt
Plaintext
Refactor all FluentValidation validators across the TakweneMusic.Application project to strictly perform pure structural/input validation without injecting or consuming IApplicationDbContext, and move all database-dependent existence and relational checks directly into their corresponding MediatR Handlers, throwing a FluentValidation ValidationException upon failure.
🚀 3. OpenAPI Documentation and Secure Client Setup (NSwag) Prompt
Plaintext
Integrate NSwag into the TakweneMusic.Api project to generate OpenAPI/Swagger documentation for our Carter Minimal APIs, including JWT/Bearer authentication configuration in the Swagger UI, and update Program.cs to register and enable the NSwag middleware.
📝 Technical Architectural Abstract
The application's core DI layout was upgraded to address a service lifetime inversion conflict (Scoped DbContext inside Singleton Validator). By transitionally moving towards a "Thin Validators, Rich Handlers" design pattern, we decoupled FluentValidation from infrastructure concerns (IApplicationDbContext). Input constraints are now strictly validated inside memory-light, scoped components, whereas relational and state existence checks are handled within MediatR handlers. This strategic split resolves all generic instantiation limits, avoids memory leaks, and prepares the API layer for automated client generation via NSwag.