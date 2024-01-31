using Demostrativo.HV.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demostrativo.HV.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PersonaController : ControllerBase
{
    private readonly ILogger<PersonaController> _logger;

    public PersonaController(ILogger<PersonaController> logger)
    {
        _logger = logger;
    }

    [HttpGet(nameof(Get))]
    public ActionResult<PersonaDto> Get()
    {
        return Ok(new PersonaDto
        {
            PersonaId = 1,
            PrimerNombre = "Duvan",
            SegundoNombre = "Smith",
        });
    }
}