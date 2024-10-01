using Inbazar.Domain.Primitives;

namespace Inbazar.Domain.Entities;

public class Basket : AggregateRoot
{
    protected Basket(Guid id, Guid userId)
        : base(id)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public Guid OrderId { get; private set; }
    public Order? Order { get; private set; }
    public IList<OrderItem>? OrderItems { get; private set; }
    public decimal Price =>
        OrderItems is not null ? OrderItems.Select(o => o.Amount * o.Balance).Sum()
        : 0;

    public static Basket Create(
        Guid id,
        Guid userId)
    {
        return new Basket(id, userId);
    }

    public void AddOrderItem(List<OrderItem> orderItems)
    {
        if (OrderItems is not null)
            ((List<OrderItem>)OrderItems).AddRange(orderItems);
        else
        {
            OrderItems = [.. orderItems];
        }
    }
}
