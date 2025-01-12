using GoldenAwards.Application.Commands.Movie.SaveMovies;
using GoldenAwards.DIFramework.Extensions;
using GoldenAwards.Infrastructure.Options;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GoldenAwards.DIFramework.Di
{
    public static class Extensions
    {
        public static IServiceCollection AddExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependenciesApplication();
            services.ConfigureDependenciesDomain();
            services.ConfigureDependenciesInfraInfrastructure(configuration);

            return services;
        }

        public static async Task InitExtensionsAsync(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var appConfig = scope.ServiceProvider.GetRequiredService<IOptions<InfraOptions>>().Value;
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            await sender.Send(new SaveMovieCommand { PathToCsvFile = appConfig.PathToCsvFile });
        }
    }
}
