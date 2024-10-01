using Inbazar.Domain.Shared;
using MediatR;

namespace Inbazar.Application.Abstractions.Messaging;

public interface IQuery<TReponse> : IRequest<Result<TReponse>>
{
}
