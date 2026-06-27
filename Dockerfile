# Build Stage using .NET 10.0 SDK (Fixes NETSDK1045 target framework build error)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
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

# Runtime Stage using .NET 10.0 ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Expose Render standard port 10000
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

# Copy published files and set Entrypoint
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TakweneMusic.Api.dll"]
