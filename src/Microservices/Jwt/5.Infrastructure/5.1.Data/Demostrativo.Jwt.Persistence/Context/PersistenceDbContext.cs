using Demostrativo.Jwt.Domain.Ejemplo;
using Demostrativo.Jwt.Persistence.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace Demostrativo.Jwt.Persistence.Context;

public class PersistenceDbContext : DbContextBase, IPersistenceDbContext
{
    public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options)
    {
    }

    public virtual DbSet<EjemploEntity> Ejemplo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EjemploEntity>().HasData(new EjemploEntity
        {
            EjemploId = 1,
            NombreEjemplo = "ejemplo",
        });
    }
}