using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Demostrativo.Jwt.Domain.Base;
using Demostrativo.Jwt.Persistence.Base.Context;
using System.Linq.Expressions;

namespace Demostrativo.Jwt.Persistence.Base;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly IDbContextBase _dbContext;
    public IUnitOfWork UnitOfWork => _dbContext;

    private bool _disposedValue = false;

    public RepositoryBase(IDbContextBase dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<TEntity>> GetAll() =>
        await _dbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetById(int id) =>
        await _dbContext.Set<TEntity>().FindAsync(id);

    public async Task<TEntity> Create(TEntity entity)
    {
        try
        {
            var newEntity = (await _dbContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false)).Entity;
            _dbContext.Commit();
            return newEntity;
        }
        catch (SqlException ex)
        {
            throw new Exception("No se pudo hacer el insert en la BD: " + ex.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.Commit();
        return true;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.Commit();
            return true;
        }
        catch (SqlException ex)
        {
            throw new Exception("No se pudo hacer el Remove en la BD: " + ex.Message);
        }
    }

    public Task<bool> Update(TEntity entidad)
    {
        _dbContext.Set<TEntity>().Update(entidad);
        _dbContext.Commit();
        return Task.FromResult(true);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicado) =>
        await _dbContext.Set<TEntity>().AnyAsync(predicado);

    public async Task<TEntity> FirstBySearchMatching(Expression<Func<TEntity, bool>> predicado) =>
        await _dbContext.Set<TEntity>().Where(predicado).FirstOrDefaultAsync();

    public async Task<TEntity> GetLastOrDefault<TResponseOrderBy>(Expression<Func<TEntity, TResponseOrderBy>> predicado) =>
        await _dbContext.Set<TEntity>().OrderBy(predicado).LastOrDefaultAsync();

    public async Task<IEnumerable<TEntity>> SearchMatching(Expression<Func<TEntity, bool>> predicado, int? skipRecords = 0, int? takeRecords = 0)
    {
        return skipRecords == 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).Take(takeRecords.Value).ToListAsync()
            : skipRecords > 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).Skip(skipRecords.Value).Take(takeRecords.Value).ToListAsync()
            : (IEnumerable<TEntity>)await _dbContext.Set<TEntity>().Where(predicado).Skip(skipRecords.Value).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> SearchMatchingOrderBy<TResponseOrderBy>(Expression<Func<TEntity, TResponseOrderBy>> predicadoOrderBy,
        Expression<Func<TEntity, bool>> predicado, int? skipRecords = 0, int? takeRecords = 0, string? orderByType = "desc")
    {
        return orderByType == "asc"
            ? skipRecords == 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).OrderBy(predicadoOrderBy).Take(takeRecords.Value).ToListAsync()
            : skipRecords > 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).OrderBy(predicadoOrderBy).Skip(skipRecords.Value).Take(takeRecords.Value).ToListAsync()
            : (IEnumerable<TEntity>)await _dbContext.Set<TEntity>().Where(predicado).OrderBy(predicadoOrderBy).Skip(skipRecords.Value).ToListAsync()
            : skipRecords == 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).OrderByDescending(predicadoOrderBy).Take(takeRecords.Value).ToListAsync()
            : skipRecords > 0 && takeRecords > 0
            ? await _dbContext.Set<TEntity>().Where(predicado).OrderByDescending(predicadoOrderBy).Skip(skipRecords.Value).Take(takeRecords.Value).ToListAsync()
            : (IEnumerable<TEntity>)await _dbContext.Set<TEntity>().Where(predicado).OrderByDescending(predicadoOrderBy).Skip(skipRecords.Value).ToListAsync();
    }

    #region IDisposable Support

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
                _dbContext.Dispose();
            _disposedValue = true;
        }
    }

    ~RepositoryBase() => Dispose(false);

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion IDisposable Support
}