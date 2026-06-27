using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TakweneMusic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(Common.Behaviors.ValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssembly(assembly, ServiceLifetime.Scoped);

        services.AddScoped<Users.Common.JwtProvider>();

        return services;
    }
}
