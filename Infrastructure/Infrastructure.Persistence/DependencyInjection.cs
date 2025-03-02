using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {


            return services;
        }
    }
}
