using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbazar.Application.Baskets.Commands;

public sealed class BasketAddItemCommandHandler : ICommandHandler<BasketAddItemCommand>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BasketAddItemCommandHandler(
        IUnitOfWork unitOfWork,
        IBasketRepository basketRepository)
    {
        _unitOfWork = unitOfWork;
        _basketRepository = basketRepository;
    }

    public async Task<Result> Handle(BasketAddItemCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.SelectById(request.BasketId);
        if (basket is null)
        {
            return Result.Failure<Basket>(new Error(
                code: "Basket.NotFound",
                message: $"Basket with Id={request.BasketId} was not found"));
        }

        basket.AddOrderItem(request.OrderItems);
        
        await _basketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(basket);
    }
}
