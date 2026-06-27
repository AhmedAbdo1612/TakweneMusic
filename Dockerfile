# Build using .NET SDK 9.0 on Alpine Linux
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy solution and project definitions
COPY TakweneMusic.slnx ./
COPY src/TakweneMusic.Domain/TakweneMusic.Domain.csproj src/TakweneMusic.Domain/
COPY src/TakweneMusic.Application/TakweneMusic.Application.csproj src/TakweneMusic.Application/
COPY src/TakweneMusic.Infrastructure/TakweneMusic.Infrastructure.csproj src/TakweneMusic.Infrastructure/
COPY src/TakweneMusic.Api/TakweneMusic.Api.csproj src/TakweneMusic.Api/

# Restore dependencies
RUN dotnet restore src/TakweneMusic.Api/TakweneMusic.Api.csproj

# Copy source code and publish
COPY src/ src/
RUN dotnet publish src/TakweneMusic.Api/TakweneMusic.Api.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

# Runtime using ASP.NET Core 9.0 on Alpine Linux (drastically smaller image size)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Install ICU libraries (required for EF Core database collation/sorting on Alpine)
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expose Render's default port 10000
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

# Copy published files and set Entrypoint
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TakweneMusic.Api.dll"]
