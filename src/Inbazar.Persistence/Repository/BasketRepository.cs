using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public sealed class BasketRepository : IBasketRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Basket> _baskets;

    public BasketRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _baskets = _dbContext.Set<Basket>();
    }

    public async Task<Basket> Insert(Basket basket) =>
        (await _baskets.AddAsync(basket)).Entity;

    public async Task<bool> Delete(Basket basket)
    {
        await Task.FromResult(_baskets.Remove(basket));
        return true;
    }

    public async Task<Basket> Update(Basket basket) =>
        (await Task.FromResult(_baskets.Update(basket))).Entity;

    public async Task<Basket> SelectById(Guid id)
    {
        var result = await _baskets
            .Include(b => b.Order)
            .Include(b => b.User)
            .Include(b => b.OrderItems)
            .FirstOrDefaultAsync(basket => basket.Id == id);

        return result;
    }

    public async Task<IEnumerable<Basket>> SelectAll()
    {
        return await Task.FromResult(
            _baskets
            .Include(b => b.Order)
            .Include(b => b.OrderItems)
            .Include(b => b.User)
            .AsEnumerable());
    }
}
