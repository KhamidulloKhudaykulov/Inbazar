namespace Inbazar.Api.Contracts;

public class ProductCreatedDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
