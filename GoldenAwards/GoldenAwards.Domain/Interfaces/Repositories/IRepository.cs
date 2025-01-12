using GoldenAwards.Domain.Models;
using System.Linq.Expressions;

namespace GoldenAwards.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TEntityKey> : IDisposable
        where TEntity : Entity<TEntityKey>
        where TEntityKey : struct
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<TEntity> Listar();
        IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>>? filtro);
        IEnumerable<TEntity> Listar(
            Expression<Func<TEntity, bool>>? filtro,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor);
        IEnumerable<TEntity> Listar(
            Expression<Func<TEntity, bool>>? filtro,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor,
            string entidades);
        Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> filtro, CancellationToken cancellationToken);
        Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> filtro, string? entidades, CancellationToken cancellationToken);
        Task<TEntity?> ObterAsync(Expression<Func<TEntity, bool>> filtro, string entidades = "", CancellationToken cancellationToken = default);
        void Inserir(TEntity entity);
        Task InserirAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Deletar(Expression<Func<TEntity, bool>> filtro);
        void Deletar(object id);
        void Deletar(TEntity entityToDelete);
        void Atualizar(TEntity entityToUpdate);
        Task<IEnumerable<TEntity>> ListarAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ListarAsync(Expression<Func<TEntity, bool>>? filtro, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ListarAsync(Expression<Func<TEntity, bool>>? filtro, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ListarAsync(Expression<Func<TEntity, bool>>? filtro, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordenarPor, string entidades, CancellationToken cancellationToken);
        Task<TEntity?> ObterPorIdAsync(TEntityKey id, string entidades = "", CancellationToken cancellationToken = default);
    }
}
