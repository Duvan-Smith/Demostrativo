using Demostrativo.Jwt.Domain.Ejemplo;
using Demostrativo.Jwt.Persistence.Base;
using Demostrativo.Jwt.Persistence.Context;

namespace Demostrativo.Jwt.Persistence.Ejemploes;

public class EjemploRepository : RepositoryBase<EjemploEntity>, IEjemploRepository
{
    public EjemploRepository(IPersistenceDbContext entityTestDbContext) : base(entityTestDbContext)
    {
    }
}