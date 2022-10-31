using BlogApp.Application.Services.Abstract;
using BlogApp.Infrastructure.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}