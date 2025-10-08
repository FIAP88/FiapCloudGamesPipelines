using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FiapCloudGamesAPI.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
        {
            services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();

            return services;
        }
    }
}
