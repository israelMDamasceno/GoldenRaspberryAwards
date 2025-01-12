using GoldenAwards.Domain.Interfaces.Repositories;
using GoldenAwards.Infrastructure.Data.Context;
using GoldenAwards.Infrastructure.Data.Repositories;
using GoldenAwards.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GoldenAwards.DIFramework.Extensions
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection ConfigureDependenciesInfraInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddDbContext<ContextDb>((sp, opts) =>
            {
                var dbConfig = sp.GetRequiredService<IOptions<InfraOptions>>().Value;

                opts
                    .UseInMemoryDatabase(dbConfig.DbAppName)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);

            return services;
        }
    }
}
