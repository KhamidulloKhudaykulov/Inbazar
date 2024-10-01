using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using MediatR;

namespace Inbazar.Application.Baskets.Commands;

public sealed class BasketCreateCommandHandler : ICommandHandler<BasketCreateCommand, Guid>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BasketCreateCommandHandler(
        IUnitOfWork unitOfWork,
        IBasketRepository basketRepository)
    {
        _unitOfWork = unitOfWork;
        _basketRepository = basketRepository;
    }

    public async Task<Result<Guid>> Handle(BasketCreateCommand request, CancellationToken cancellationToken)
    {
        var basket = Basket.Create(
            Guid.NewGuid(),
            request.UserId);

        await _basketRepository.Insert(basket);
        await _unitOfWork.SaveChangesAsync();

        return basket.Id;
    }
}
