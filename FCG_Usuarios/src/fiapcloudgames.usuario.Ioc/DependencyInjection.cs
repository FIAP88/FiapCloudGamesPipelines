using fiapcloudgames.usuario.Application.Services;
using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using fiapcloudgames.usuario.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fiapcloudgames.usuario.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnFiapCloudGames");

            services.AddDbContext<FiapCloudGamesDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString, sqlOptions => sqlOptions.CommandTimeout(40))
            );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            return services;
        }
    }
}
