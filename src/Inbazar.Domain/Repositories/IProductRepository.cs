using Inbazar.Domain.Entities;

namespace Inbazar.Domain.Repositories;

public interface IProductRepository
{
    Task Insert(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
    Task<Product> SelectById(Guid id);
    Task<IEnumerable<Product>> SelectAll();
    Task<IEnumerable<Product>> GetAll();
}
