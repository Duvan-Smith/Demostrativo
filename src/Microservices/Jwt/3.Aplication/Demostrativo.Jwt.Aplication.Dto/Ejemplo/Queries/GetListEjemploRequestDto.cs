using MediatR;
using Demostrativo.Jwt.Aplication.Dto.Base;

namespace Demostrativo.Jwt.Aplication.Dto.Ejemplo.Queries;

public class GetListEjemploRequestDto : IRequest<GenericResponseDto<List<EjemploDto>>>
{
}