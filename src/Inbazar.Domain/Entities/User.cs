using Inbazar.Domain.DomainEvents;
using Inbazar.Domain.Primitives;
using Inbazar.Domain.ValueObjects.User;

namespace Inbazar.Domain.Entities;

public class User : AggregateRoot
{
    protected User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email,
        string phone,
        string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Password = password;
    }

    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }
    public Email? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Password { get; private set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Basket>? Baskets { get; set; }

    public static User Create(
        FirstName FirstName,
        LastName LastName,
        Email Email,
        string Phone,
        string Password)
    {


        var userResult = new User(
            Guid.NewGuid(),
            FirstName,
            LastName,
            Email,
            Phone,
            Password);

        userResult.RaiseDomainEvent(new UserCreatedDomainEvent(
            Guid.NewGuid(),
            userResult.Id));

        return userResult;
    }
}
