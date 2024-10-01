namespace Inbazar.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}
