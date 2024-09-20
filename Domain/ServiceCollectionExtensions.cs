using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatrService(this IServiceCollection services)
        {
            //Registra todos os handlers do MediatR
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
        }
    }
}
