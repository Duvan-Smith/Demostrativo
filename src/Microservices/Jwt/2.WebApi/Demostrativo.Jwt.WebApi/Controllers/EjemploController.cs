using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Queries;
using Demostrativo.Jwt.WebApi.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demostrativo.Jwt.WebApi.Controllers;

[Route("jwt/[controller]")]
public class EjemploController : DemostrativoControllerBase
{
    private readonly IMediator _mediator;

    public EjemploController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet(nameof(GetListEjemplo))]
    public async Task<ActionResult<GenericResponseDto<List<EjemploDto>>>> GetListEjemplo()
    {
        try
        {
            var result = await _mediator
                .Send(new GetListEjemploRequestDto())
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<List<EjemploDto>>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = null
            });
        }
    }

    [HttpGet(nameof(GetByNameEjemplo))]
    public async Task<ActionResult<GenericResponseDto<EjemploDto>>> GetByNameEjemplo([FromQuery] GetByNameEjemploRequestDto getByNameEjemploRequestDto)
    {
        try
        {
            var result = await _mediator
                .Send(getByNameEjemploRequestDto)
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<EjemploDto>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = null
            });
        }
    }

    [HttpGet(nameof(GetByIdEjemplo))]
    public async Task<ActionResult<GenericResponseDto<EjemploDto>>> GetByIdEjemplo([FromQuery] GetByIdEjemploRequestDto getByIdEjemploRequestDto)
    {
        try
        {
            var result = await _mediator
                .Send(getByIdEjemploRequestDto)
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<EjemploDto>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = null
            });
        }
    }

    [HttpPost(nameof(CreateEjemplo))]
    public async Task<ActionResult<GenericResponseDto<EjemploDto>>> CreateEjemplo(CreateEjemploRequestDto entity)
    {
        try
        {
            var result = await _mediator
                .Send(entity)
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<EjemploDto>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = null
            });
        }
    }

    [HttpPut(nameof(UpdateEjemplo))]
    public async Task<ActionResult<GenericResponseDto<bool>>> UpdateEjemplo(UpdateEjemploRequestDto entity)
    {
        try
        {
            var result = await _mediator
                .Send(entity)
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<bool>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = false
            });
        }
    }

    [HttpDelete(nameof(DeleteEjemplo))]
    public async Task<ActionResult<GenericResponseDto<bool>>> DeleteEjemplo([FromQuery] DeleteEjemploRequestDto deleteEjemploRequestDto)
    {
        try
        {
            var result = await _mediator
                .Send(deleteEjemploRequestDto)
                .ConfigureAwait(false);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : BadRequest(result);
        }
        catch (Exception ex)
        {
            var listError = new List<string>()
            {
                ex.Message
            };
            return BadRequest(new GenericResponseDto<bool>
            {
                Errors = listError.Count > 0 ? listError : null,
                StatusCode = listError.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK,
                StatusDescription = listError.Count > 0 ? "Sin resultados." : "Correcto.",
                Result = false
            });
        }
    }
}