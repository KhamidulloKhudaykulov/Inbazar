using Inbazar.Domain.Entities;

namespace Inbazar.Domain.Repositories;

public interface ICategoryRepository
{
    Task Insert(Category category);
    Task Update(Category category);
    Task Delete(Category category);
    Task<Category> SelectById(Guid id);
    Task<IEnumerable<Category>> SelectAll();
}
