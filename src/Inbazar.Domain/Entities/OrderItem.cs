namespace Inbazar.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public Product? Product { get; set; }
    public Basket? Basket { get; set; }
    public int Amount { get; set; }
    public decimal Balance =>
        Product!.Price!.Value * Amount;

    public void Add(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Please enter correctly amount");

        Amount += amount;
    }
}
