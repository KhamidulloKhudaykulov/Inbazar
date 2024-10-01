using Inbazar.Domain.Repositories;
using Inbazar.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Inbazar.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        services.AddScoped(typeof(IOrderItemRepository), typeof(OrderItemRepository));
        services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
        services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
        services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        return services;
    }
}
