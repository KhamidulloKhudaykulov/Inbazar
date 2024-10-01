using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;

namespace Inbazar.Application.Orders.Commands;

public sealed class OrderCreateCommandHandler : ICommandHandler<OrderCreateCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreateCommandHandler(
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository,
        IBasketRepository basketRepository,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _basketRepository = basketRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectById(request.UserId);
        if (user is null)
        {
            return Result.Failure<User>(new Error(
                code: "User.NotFound",
                message: $"This user with Id={request.UserId} was not found"));
        }

        var basket = await _basketRepository.SelectById(request.BasketId);
        if (basket is null) 
        {
            return Result.Failure<Basket>(new Error(
                code: "Basket.NotFound",
                message: $"Basket with Id={request.BasketId} was not found"));
        }

        var order = Order.Create(
            Guid.NewGuid(),
            request.UserId,
            request.BasketId);

        await _orderRepository.InsertAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(order);
    }
}
