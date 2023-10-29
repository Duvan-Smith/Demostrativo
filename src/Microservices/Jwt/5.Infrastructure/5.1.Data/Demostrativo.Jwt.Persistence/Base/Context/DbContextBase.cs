using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Demostrativo.Jwt.Persistence.Base.Context;

public class DbContextBase : DbContext, IDisposable
{
    private readonly DbContextOptions _options;

    public DbContextBase(DbContextOptions options) => _options = options;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var extensionNpgsql = _options.FindExtension<NpgsqlOptionsExtension>();
        if (extensionNpgsql != null)
            optionsBuilder.UseNpgsql(extensionNpgsql.ConnectionString);

        var extensionSqlServer = _options.FindExtension<SqlServerOptionsExtension>();
        if (extensionSqlServer != null)
            optionsBuilder.UseSqlServer(extensionSqlServer.ConnectionString);
    }

    #region IUnitOfWork

    public int Commit() => base.SaveChanges();

    public void UndoChanges()
    {
        base.ChangeTracker.Entries()
        .Where(e => e.Entity != null).ToList()
        .ForEach(e => e.State = EntityState.Detached);
    }

    public new DbSet<TGenericEntity> Set<TGenericEntity>() where TGenericEntity : class =>
        base.Set<TGenericEntity>();

    public void AttachEntity<TGenericEntity>(TGenericEntity item) where TGenericEntity : class
    {
        if (Entry(item).State == EntityState.Detached)
            base.Set<TGenericEntity>().Attach(item);
    }

    public void SetModified<TGenericEntity>(TGenericEntity item) where TGenericEntity : class =>
        Entry(item).State = EntityState.Modified;

    #endregion IUnitOfWork
}