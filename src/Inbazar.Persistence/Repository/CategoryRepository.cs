using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Category> _categories;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _categories = _dbContext.Set<Category>();
    }

    public async Task Delete(Category category)
        => await Task.FromResult(_categories.Remove(category));

    public async Task Insert(Category category)
        => await _categories.AddAsync(category);

    public async Task<IEnumerable<Category>> SelectAll()
        => await Task.FromResult(_categories);

    public async Task<Category> SelectById(Guid id)
    {
        var result = await _categories.FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task Update(Category category)
        => await Task.FromResult(_categories.Update(category));
}
