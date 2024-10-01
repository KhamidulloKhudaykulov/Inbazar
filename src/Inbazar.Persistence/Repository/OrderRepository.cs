using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Order> _orders;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        _orders.Remove((await _orders.FirstOrDefaultAsync(x => x.Id == id))!);
        return true;
    }

    public async Task<Order> InsertAsync(Order order)
    {
        return (await _orders.AddAsync(order)).Entity;
    }

    public async Task<IEnumerable<Order>> SelectAllAsync()
    {
        return await Task.FromResult(_orders);
    }

    public async Task<Order> SelectByIdAsync(Guid id)
    {
        return await _orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        return await Task.FromResult(_orders.Update(order).Entity);
    }
}
