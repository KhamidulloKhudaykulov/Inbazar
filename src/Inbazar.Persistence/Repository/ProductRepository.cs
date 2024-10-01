using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Product> _products;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
        _products = _context.Set<Product>();
    }
    public async Task Insert(Product product)
    {
        await _products.AddAsync(product);
    }

    public async Task Delete(Guid id)
    {
        var result = await _products.FirstOrDefaultAsync(p => p.Id == id);

        _products.Remove(result!);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await Task.FromResult(_products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .AsEnumerable());
    }

    public async Task<Product> SelectById(Guid id)
    {
        var result = await _products
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id);

        return result;
    }

    public async Task<IEnumerable<Product>> SelectAll()
    {
        return (await Task.FromResult(_products.Include(p => p.ProductCategories)));
    }

    public async Task Update(Product product)
    {
        await Task.FromResult(_products.Update(product));
    }
}
