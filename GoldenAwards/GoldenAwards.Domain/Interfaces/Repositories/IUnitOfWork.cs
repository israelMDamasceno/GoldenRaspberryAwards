namespace GoldenAwards.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<TResponse?> ExecuteTransactionAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken);
        Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
