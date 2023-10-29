namespace Demostrativo.HV.WebApi.Models;

public class PersonaDto
{
    public int PersonaId { get; set; }
    public string PrimerNombre { get; set; } = default!;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = default!;
    public string? SegundoApellido { get; set; }
    public string? Foto { get; set; }
    public DateTime FechaNacimiento { get; set; }
}