using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<OrderItem> _orders;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
        _orders = _context.Set<OrderItem>();
    }

    public async Task DeleteAsync(Guid id)
        => _orders.Remove((await _orders.FindAsync(id))!);

    public async Task<OrderItem> InsertAsync(OrderItem item)
    {
        var result = await _context.AddAsync(item);
        return result.Entity;
    }

    public async Task<IEnumerable<OrderItem>> SelectAllAsync()
        => await Task.FromResult(_orders);

    public async Task<OrderItem> SelectByIdAsync(Guid id)
        => await _orders
        .Include(oi => oi.Basket)
        .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<OrderItem> UpdateAsync(OrderItem item)
        => await Task.FromResult(_orders.Update(item).Entity);
}
