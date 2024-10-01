using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;

namespace Inbazar.Application.OrderItems.Commands;

public sealed class ChangeProductAmountCommandHandler : ICommandHandler<ChangeProductAmountCommand>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeProductAmountCommandHandler(
        IUnitOfWork unitOfWork,
        IOrderItemRepository orderItemRepository,
        IBasketRepository basketRepository)
    {
        _unitOfWork = unitOfWork;
        _orderItemRepository = orderItemRepository;
        _basketRepository = basketRepository;
    }
    public async Task<Result> Handle(ChangeProductAmountCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.SelectById(request.BasketId);
        if (basket is null)
        {
            return Result.Failure<Basket>(new Error(
                code: "Basket.NotFound",
                message: $"This basket with id={request.BasketId} was not found"));
        }

        var orderItem = await _orderItemRepository.SelectByIdAsync(request.OrderItemId);
        if (orderItem is null)
        {
            return Result.Failure<OrderItem>(new Error(
                code: "OrderItem.NotFound",
                message: $"This order item with id={request.OrderItemId} was not found"));
        }

        if (!request.up)
        {
            if (orderItem.Amount < request.amount)
            {
                return Result.Failure<OrderItem>(new Error(
                    code: "OrderItemAmount.TooMuch",
                    message: $"Please enter correctly amount"));
            }

            orderItem.Amount -= request.amount;
        }
        else
        {
            orderItem.Amount += request.amount;
        }

        await _orderItemRepository.UpdateAsync(orderItem);
        await _basketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(orderItem);
    }
}
