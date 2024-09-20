using Domain.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class BaseConfig
{
    protected void InitializeMediatrService()
    {
        var services = new ServiceCollection();
        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ProdutoCreateHandler).Assembly))
            .BuildServiceProvider();

    }
}
