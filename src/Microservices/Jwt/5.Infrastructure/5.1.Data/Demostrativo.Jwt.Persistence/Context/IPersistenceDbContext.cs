using Demostrativo.Jwt.Domain.Ejemplo;
using Demostrativo.Jwt.Persistence.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace Demostrativo.Jwt.Persistence.Context;

public interface IPersistenceDbContext : IDbContextBase
{
    DbSet<EjemploEntity> Ejemplo { get; }

    #region
    //Comando para agregar migracion desde Consola vs: add-migration "init" -context PersistenceDbContext
    //Comando para agregar migracion desde Consola terminal: 
    //  dotnet ef --startup-project ../../../2.WebApi/Demostrativo.Jwt.WebApi --verbose migrations add Initial --context PersistenceDbContext
    #endregion 
}