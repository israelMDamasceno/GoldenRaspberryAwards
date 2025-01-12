using GoldenAwards.Domain.Interfaces.Services;
using GoldenAwards.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoldenAwards.DIFramework.Extensions
{
    public static class ConfigureDomain
    {
        public static IServiceCollection ConfigureDependenciesDomain(this IServiceCollection services)
        {
            services.AddScoped<IMoviesService, MoviesService>();

            return services;
        }
    }
}
