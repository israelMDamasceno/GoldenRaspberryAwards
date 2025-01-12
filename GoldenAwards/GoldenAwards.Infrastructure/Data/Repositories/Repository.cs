using GoldenAwards.Domain.Interfaces.Repositories;
using GoldenAwards.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoldenAwards.Infrastructure.Data.Repositories
{
    public class Repository<TEntity, TEntityKey, TContext> : IRepository<TEntity, TEntityKey>
       where TEntity : Entity<TEntityKey>
       where TEntityKey : struct
       where TContext : DbContext, IUnitOfWork
    {
        protected static readonly char[] separator = [','];
        protected readonly TContext context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Listar()
        {
            return Listar(null);
        }

        public IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>>? filtro)
        {
            return Listar(filtro, null);
        }

        public IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>>? filtro, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor)
        {
            return Listar(filtro, ordenarPor, string.Empty);
        }

        public virtual IEnumerable<TEntity> Listar(
            Expression<Func<TEntity, bool>>? filtro,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor,
            string entidades)
        {
            IQueryable<TEntity> query = dbSet;

            if (filtro is not null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in entidades.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (ordenarPor != null)
            {
                return [.. ordenarPor(query)];
            }
            else
            {
                return [.. query];
            }
        }

        public async Task<IEnumerable<TEntity>> ListarAsync(CancellationToken cancellationToken)
        {
            return await ListarAsync(null, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ListarAsync(
            Expression<Func<TEntity, bool>>? filtro,
            CancellationToken cancellationToken)
        {
            return await ListarAsync(filtro, null, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ListarAsync(
            Expression<Func<TEntity, bool>>? filtro,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>>? ordenarPor,
            CancellationToken cancellationToken)
        {
            return await ListarAsync(filtro, ordenarPor, string.Empty, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> ListarAsync(
            Expression<Func<TEntity, bool>>? filtro,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor,
            string entidades,
            CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = dbSet;

            if (filtro is not null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in entidades.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (ordenarPor != null)
            {
                return await ordenarPor(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }

        public virtual async Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> filtro, CancellationToken cancellationToken)
        {
            return await ExisteAsync(filtro, null, cancellationToken);
        }

        public virtual async Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> filtro, string? entidades, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(entidades))
            {
                foreach (var includeProperty in entidades.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AnyAsync(filtro, cancellationToken);
        }

        public virtual async Task<TEntity?> ObterAsync(Expression<Func<TEntity, bool>> filtro, string entidades = "", CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in entidades.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(filtro, cancellationToken);
        }

        public virtual void Inserir(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual async Task InserirAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task<TEntity?> ObterPorIdAsync(TEntityKey id, string entidades = "", CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in entidades.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            query = query.Where(x => x.Id.Equals(id));

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual void Deletar(Expression<Func<TEntity, bool>> filtro)
        {
            var entities = dbSet.Where(filtro);

            entities.ForEachAsync(entityToDelete =>
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
            });

            dbSet.RemoveRange(entities);
        }

        public virtual void Deletar(object id)
        {
            var entityToDelete = dbSet.Find(id);

            if (entityToDelete is not null)
            {
                Deletar(entityToDelete);
            }
        }

        public virtual void Deletar(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }

        public virtual void Atualizar(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IUnitOfWork UnitOfWork => context;

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose
    }
}
