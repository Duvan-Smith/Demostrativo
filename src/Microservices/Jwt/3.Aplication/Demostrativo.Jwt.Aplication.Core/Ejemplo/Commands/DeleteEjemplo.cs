using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;
using Demostrativo.Jwt.Domain.Ejemplo;
using MediatR;
using System.Net;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes.Commands;

public class DeleteEjemplo : IRequestHandler<DeleteEjemploRequestDto, GenericResponseDto<bool>>
{
    private readonly IMediator _mediator;
    private readonly IEjemploRepository _EjemploRepository;

    public DeleteEjemplo(IMediator mediator, IEjemploRepository EjemploRepository)
    {
        _mediator = mediator;
        _EjemploRepository = EjemploRepository;
    }

    public async Task<GenericResponseDto<bool>> Handle(DeleteEjemploRequestDto request, CancellationToken cancellationToken)
    {
        var listErrors = new List<string>();

        var result = false;

        try
        {
            result = await _EjemploRepository.Delete(request.EjemploId);
        }
        catch (Exception ex)
        {
            listErrors.Add("Error Ejemplos-Ejemplo-Delete");
            if (string.IsNullOrEmpty(ex.InnerException?.Message))
                listErrors.Add(ex.Message);
            else
                listErrors.Add(ex.InnerException.Message);
        }

        if (!result)
            listErrors.Add("Error Ejemplos-Ejemplo-Delete-false");

        return new GenericResponseDto<bool>()
        {
            Result = result,
            StatusCode = listErrors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            StatusDescription = listErrors.Count == 0 ? "Correcto" : "Error",
            Errors = listErrors.Count == 0 ? null : listErrors,
        };
    }
}