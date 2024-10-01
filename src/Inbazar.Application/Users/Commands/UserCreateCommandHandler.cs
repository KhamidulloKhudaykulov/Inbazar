using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using Inbazar.Domain.ValueObjects.User;

namespace Inbazar.Application.Users.Commands;

public class UserCreateCommandHandler : ICommandHandler<UserCreateCommand, UserCreateCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreateCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserCreateCommand>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        Result<FirstName> firstName = FirstName.Create(request.FirstName);
        if (firstName.IsFailure)
            return Result.Failure<UserCreateCommand>(firstName.Error);

        Result<LastName> lastName = LastName.Create(request.LastName);
        if (lastName.IsFailure)
            return Result.Failure<UserCreateCommand>(lastName.Error);

        Result<Email> email = Email.Create(request.Email);
        if (email.IsFailure)
            return Result.Failure<UserCreateCommand>(email.Error);

        if (!await _repository.IsEmailUnique(email.Value))
        {
            return Result.Failure<UserCreateCommand>(new Error(
                "Email.AlreadyInUse", "This email was already registered"));
        }

        var user = User.Create(
            firstName.Value,
            lastName.Value,
            email.Value,
            request.Phone,
            request.Password);

        await _repository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(request);
    }
}
