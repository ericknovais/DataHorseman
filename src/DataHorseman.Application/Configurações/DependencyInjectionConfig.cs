using DataHorseman.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataHorseman.Application.Configurações;

public static class DependencyInjectionConfig
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Registra os serviços da camada Application
        services.AddScoped<Application.Interfaces.IService, Application.Services.Service>();
        //services.AddScoped<IRepository, Repository>();
        services.AddAutoMapper(typeof(Application.Profiles.AtivoProfile).Assembly);
    }
}