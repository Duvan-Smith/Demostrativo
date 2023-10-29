using System.Linq.Expressions;

namespace Demostrativo.Jwt.Domain.Base;

public interface IRepositoryBase<TEntity> : IDisposable
{
    IUnitOfWork UnitOfWork { get; }

    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> GetById(int id);

    Task<TEntity> Create(TEntity entidad);

    Task<bool> Delete(int id);

    Task<bool> Delete(TEntity entidad);

    Task<bool> Update(TEntity entidad);

    Task<bool> Any(Expression<Func<TEntity, bool>> predicado);

    Task<TEntity> FirstBySearchMatching(Expression<Func<TEntity, bool>> predicado);

    Task<TEntity> GetLastOrDefault<TResponseOrderBy>(Expression<Func<TEntity, TResponseOrderBy>> predicado);

    Task<IEnumerable<TEntity>> SearchMatching(Expression<Func<TEntity, bool>> predicado, int? skipRecords = 0, int? takeRecords = 0);

    Task<IEnumerable<TEntity>> SearchMatchingOrderBy<TResponseOrderBy>(Expression<Func<TEntity, TResponseOrderBy>> predicadoOrderBy,
        Expression<Func<TEntity, bool>> predicado, int? skipRecords = 0, int? takeRecords = 0, string? orderByType = "asc");
}