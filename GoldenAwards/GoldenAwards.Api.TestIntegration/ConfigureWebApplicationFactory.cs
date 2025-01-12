using GoldenAwards.Infrastructure.Data.Context;
using GoldenAwards.Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GoldenAwards.Api.TestIntegration
{
    internal class ConfigureWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string pathToCsv;

        public ConfigureWebApplicationFactory(string pathToCsv)
        {
            this.pathToCsv = pathToCsv;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(async services =>
            {
                var dbContext = CreateContext(services);
                await dbContext.Database.EnsureDeletedAsync();

                services.Configure<InfraOptions>(opts =>
                {
                    opts.PathToCsvFile = pathToCsv;
                });
            });
        }

        private static ContextDb CreateContext(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ContextDb>();

            return dbContext;
        }
    }
}


