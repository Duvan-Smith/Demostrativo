using MediatR;
using Demostrativo.Jwt.Aplication.Dto.Base;

namespace Demostrativo.Jwt.Aplication.Dto.Ejemplo.Queries;

public class GetByIdEjemploRequestDto : IRequest<GenericResponseDto<EjemploDto>>
{
    public int EjemploId { get; set; }
}