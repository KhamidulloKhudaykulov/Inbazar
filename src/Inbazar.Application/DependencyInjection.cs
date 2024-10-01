using Microsoft.Extensions.DependencyInjection;

namespace Inbazar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
