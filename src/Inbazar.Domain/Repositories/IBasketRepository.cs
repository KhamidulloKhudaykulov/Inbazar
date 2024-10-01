using Inbazar.Domain.Entities;

namespace Inbazar.Domain.Repositories;

public interface IBasketRepository
{
    Task<Basket> Insert(Basket basket);
    Task<Basket> Update(Basket basket);
    Task<bool> Delete(Basket basket);
    Task<Basket> SelectById(Guid id);
    Task<IEnumerable<Basket>> SelectAll();
}
