using Inbazar.Domain.Primitives;

namespace Inbazar.Domain.Entities;

public class Order : AggregateRoot
{
    private Order(
        Guid id,
        Guid userId,
        Guid basketId) : base(id)
    {
        UserId = userId;
        BasketId = basketId;
    }

    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid BasketId { get; set; }
    public Basket? Basket { get; set; }

    public static Order Create(
        Guid id,
        Guid userId,
        Guid basketId
        )
    {

        return new Order(id, userId, basketId);
    }
}
