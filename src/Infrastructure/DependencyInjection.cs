using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void InfrastructureDependencyInjection(this IServiceCollection services)
        {
            // Register repositories
            services.AddRepositories();

            // Register services
            services.AddSingleton<MiddlewareRegistrationService>();
            services.AddHttpClient<ExternalSaleService>();
            services.AddHttpClient<ExternalRessourceService>();
            services.AddScoped<SyncProjectsService>();
            services.AddHostedService<SyncProjectsBackgroundService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IExternalRessourceService, ExternalRessourceService>();
            services.AddScoped<ISaleRepository, SaleRepository>();
        }
    }
}
