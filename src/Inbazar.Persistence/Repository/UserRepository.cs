using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<User> _set;
    public UserRepository(ApplicationDbContext context)
    {
        _dbContext = context;
        _set = context.Set<User>();

    }
    public async Task Add(User user)
    {
        await _set.AddAsync(user);
    }

    public async Task Delete(Guid id)
    {
        _set.Remove((await _set.FirstOrDefaultAsync(x => x.Id == id))!);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await Task.FromResult(
            _set.Include(u => u.Orders)
            .AsEnumerable());
    }

    public async Task<User> SelectById(Guid id)
    {
        var result = await _set
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<bool> IsEmailUnique(Email email)
    {
        return !(await _set.AnyAsync(x => x.Email == email));
    }

    public async Task Update(User user)
    {
        await Task.FromResult(_set.Update(user));
    }
}
