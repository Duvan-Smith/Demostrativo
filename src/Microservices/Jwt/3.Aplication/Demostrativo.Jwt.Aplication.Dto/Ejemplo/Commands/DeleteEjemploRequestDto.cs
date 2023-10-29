using MediatR;
using Demostrativo.Jwt.Aplication.Dto.Base;

namespace Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;

public class DeleteEjemploRequestDto : IRequest<GenericResponseDto<bool>>
{
    public int EjemploId { get; set; }
}