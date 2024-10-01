using Inbazar.Domain.Entities;
using Inbazar.Domain.ValueObjects.User;

namespace Inbazar.Domain.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task Update(User user);
    Task Delete(Guid id);
    Task<User> SelectById(Guid id);
    Task<bool> IsEmailUnique(Email email);
    Task<IEnumerable<User>> GetAll();
}
