using GoldenAwards.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldenAwards.Infrastructure.Data.Context
{
    public class BaseDbContext<T> : DbContext, IUnitOfWork where T : DbContext
    {
        public BaseDbContext(DbContextOptions<T> options) : base(options)
        { }

        #region UnitOfWork
        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            var executionStrategy = Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
                await action();
                await transaction.CommitAsync(cancellationToken);
            });
        }

        public async Task<TResponse?> ExecuteTransactionAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken)
        {
            var executionStrategy = Database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var response = await action();
                    await transaction.CommitAsync(cancellationToken);
                    return response;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

        public int Commit()
        {
            return SaveChanges();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await SaveChangesAsync(cancellationToken);
        }
        #endregion UnitOfWork
    }
}
