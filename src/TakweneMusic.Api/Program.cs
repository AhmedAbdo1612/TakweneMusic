using TakweneMusic.Api.Endpoints.Common;
using TakweneMusic.Api.Middlewares;
using TakweneMusic.Application;
using TakweneMusic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register Clean Architecture Layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "v1";
    config.Title = "TakweneMusic API";
    config.Version = "v1";

    // Add JWT Bearer Security Definition
    config.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    config.OperationProcessors.Add(new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
app.UseOpenApi();
app.UseSwaggerUi(settings =>
{
    settings.Path = "/swagger";
    settings.DocumentPath = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var endpointGroups = typeof(Program).Assembly
    .GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointGroup).IsAssignableFrom(t));

foreach (var groupType in endpointGroups)
{
    var endpointGroup = (IEndpointGroup)Activator.CreateInstance(groupType)!;
    endpointGroup.MapEndpoints(app);
}

app.Run();
