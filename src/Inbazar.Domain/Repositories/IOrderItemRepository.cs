using Inbazar.Domain.Entities;

namespace Inbazar.Domain.Repositories;

public interface IOrderItemRepository
{
    Task<OrderItem> InsertAsync(OrderItem item);
    Task<OrderItem> UpdateAsync(OrderItem item);
    Task DeleteAsync(Guid id);
    Task<OrderItem> SelectByIdAsync(Guid id);
    Task<IEnumerable<OrderItem>> SelectAllAsync();
}
