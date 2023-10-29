using AutoMapper;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;
using Demostrativo.Jwt.Domain.Ejemplo;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes;

public class EjemploMapperProfile : Profile
{
    public EjemploMapperProfile()
    {
        CreateMap<EjemploEntity, EjemploDto>().ReverseMap();
        CreateMap<EjemploEntity, CreateEjemploRequestDto>().ReverseMap();
        CreateMap<EjemploEntity, UpdateEjemploRequestDto>().ReverseMap();
    }
}