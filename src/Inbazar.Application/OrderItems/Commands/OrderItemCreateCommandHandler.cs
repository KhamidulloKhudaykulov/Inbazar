using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Application.Baskets.Commands;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using MediatR;

namespace Inbazar.Application.OrderItems.Commands;

public sealed class OrderItemCreateCommandHandler : ICommandHandler<OrderItemCreateCommand>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISender _sender;
    private readonly IUnitOfWork _unitOfWork;

    public OrderItemCreateCommandHandler(
        IUnitOfWork unitOfWork,
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository,
        ISender sender)
    {
        _unitOfWork = unitOfWork;
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
        _sender = sender;
    }

    public async Task<Result> Handle(OrderItemCreateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.SelectById(request.ProductId);
        if (product is null)
        {
            return Result.Failure<Product>(new Error(
                code: "Product.NotFound",
                message: $"This product with id={request.ProductId} was not found"));
        }

        var orderItem = new OrderItem()
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            Product = product,
            Amount = request.Amount,
        };

        await _orderItemRepository.InsertAsync(orderItem);
        await _unitOfWork.SaveChangesAsync();

        var basket = await _sender.Send(new BasketCreateCommand(
            request.UserId,
            new List<OrderItem> { orderItem }));

        orderItem.BasketId = basket.Value;
        await _orderItemRepository.UpdateAsync(orderItem);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
