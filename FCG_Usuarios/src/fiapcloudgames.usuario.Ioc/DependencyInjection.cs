using fiapcloudgames.usuario.Application.Services;
using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Dispatcher;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using fiapcloudgames.usuario.Infrastructure.Projections.Projector;
using FiapCloudGamesAPI.EventStore.Infra;
using FiapCloudGamesAPI.EventStore.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fiapcloudgames.usuario.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
			//Integração
			var connectionStringEventStore = configuration.GetConnectionString("EventStore");

			services.AddDbContext<EventStoreDbContext>(options =>
				options.UseLazyLoadingProxies().UseSqlServer(connectionStringEventStore, sqlOptions => sqlOptions.CommandTimeout(40))
			);

			var connectionStringReadModelDb = configuration.GetConnectionString("ReadModel");

			services.AddDbContext<ReadModelDbContext>(options =>
				options.UseLazyLoadingProxies().UseSqlServer(connectionStringReadModelDb, sqlOptions => sqlOptions.CommandTimeout(40))
			);

			services.AddScoped<IEventDispatcher, EventDispatcher>();
			services.AddScoped<IProjector, UsuarioAggregateReadModelProjector>();
            services.AddScoped<IProjector, UsuarioAggregateLoginReadModelProjector>();
            services.AddScoped<IUsuarioAggregateRepository, UsuarioAggregateRepository>();
			services.AddScoped<IEventStore, SqlEventStore>();
			//
            //services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();

			return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            return services;
        }
    }
}
