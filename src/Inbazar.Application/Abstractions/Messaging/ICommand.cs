using Inbazar.Domain.Shared;
using MediatR;

namespace Inbazar.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }
