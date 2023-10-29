using MediatR;
using Demostrativo.Jwt.Aplication.Dto.Base;
using System.ComponentModel.DataAnnotations;

namespace Demostrativo.Jwt.Aplication.Dto.Ejemplo.Queries;

public class GetByNameEjemploRequestDto : IRequest<GenericResponseDto<EjemploDto>>
{
    [Required(ErrorMessage = "El campo NombreEjemplo es obligatorio")]
    public string NombreEjemplo { get; set; } = default!;
}