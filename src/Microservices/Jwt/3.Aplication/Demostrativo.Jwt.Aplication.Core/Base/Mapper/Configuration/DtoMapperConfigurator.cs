using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Demostrativo.Jwt.Aplication.Core.Base.Mapper.Configuration;

public static class DtoMapperConfigurator
{
    public static void ConfigureMapper(this IServiceCollection serviceList)
    {
        if (serviceList.All(d => d.ServiceType != typeof(IMapper)))
        {
            serviceList.TryAddSingleton(p =>
            {
                var parameterlessMappers = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsInterface &&
                                t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(t => (Profile)Activator.CreateInstance(t)!);

                var serviceMappers = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsInterface &&
                                t.GetConstructor(new[] { typeof(IServiceProvider) }) != null)
                    .Select(t => (Profile)Activator.CreateInstance(t, p)!);

                return new MapperConfiguration(cfg => cfg.AddProfiles(parameterlessMappers.Concat(serviceMappers))).CreateMapper();
            });
        }
    }
}