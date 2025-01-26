using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void InfrastructureDependencyInjection(this IServiceCollection services)
        {
            services.AddRepositories();

        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRessourceRepository, RessourceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

        }
    }
}
