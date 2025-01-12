using GoldenAwards.Application.Commands.Movie.SaveMovies;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GoldenAwards.DIFramework.Extensions
{
    public static class ConfigureApplication
    {
        public static IServiceCollection ConfigureDependenciesApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),typeof(SaveMovieCommandHandle).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
