using MediatR;
using Demostrativo.Jwt.Aplication.Dto.Base;
using System.ComponentModel.DataAnnotations;

namespace Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;

public class UpdateEjemploRequestDto : IRequest<GenericResponseDto<bool>>
{
    public int EjemploId { get; set; }

    [Required(ErrorMessage = "El campo NombreEjemplo es obligatorio")]
    public string NombreEjemplo { get; set; } = default!;
}