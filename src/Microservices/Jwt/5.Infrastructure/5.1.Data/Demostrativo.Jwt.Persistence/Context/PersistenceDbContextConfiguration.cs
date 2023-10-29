using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Demostrativo.Jwt.Persistence.Context;

namespace Demostrativo.Jwt.PhotoDetection.Persistence.EntityTest.Context;

public static class PersistenceDbContextConfiguration
{
    public static void ConfigurationPersistenceDbContext(this IServiceCollection services, string connectionString)
    {
        services.TryAddScoped<IPersistenceDbContext, PersistenceDbContext>();
        services.AddDbContextFactory<PersistenceDbContext>(x => x.UseNpgsql(connectionString));
    }
}