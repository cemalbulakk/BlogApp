using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}