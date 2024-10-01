using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;

namespace Inbazar.Application.Users.Commands;

public sealed class UserUpdateCommandHandler : ICommandHandler<UserUpdateCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserUpdateCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result.Success());
    }
}
