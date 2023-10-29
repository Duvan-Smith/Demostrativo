using Demostrativo.Jwt.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demostrativo.Jwt.Domain.Ejemplo;

public class EjemploEntity : EntityBase
{
    [Key]
    public int EjemploId { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "El campo NombreEjemplo es obligatorio")]
    public string NombreEjemplo { get; set; } = default!;
}