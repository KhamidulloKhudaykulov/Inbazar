using Inbazar.Domain.Entities;

namespace Inbazar.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> InsertAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task<bool> DeleteAsync(Guid id);
    Task<Order> SelectByIdAsync(Guid id);
    Task<IEnumerable<Order>> SelectAllAsync();
}
