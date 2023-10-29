using AutoMapper;
using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;
using Demostrativo.Jwt.Domain.Ejemplo;
using MediatR;
using System.Net;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes.Commands;

public class CreateEjemplo : IRequestHandler<CreateEjemploRequestDto, GenericResponseDto<EjemploDto>>
{
    private readonly IMapper _mapper;
    private readonly IEjemploRepository _EjemploRepository;

    public CreateEjemplo(IMapper mapper, IEjemploRepository EjemploRepository)
    {
        _mapper = mapper;
        _EjemploRepository = EjemploRepository;
    }

    public async Task<GenericResponseDto<EjemploDto>> Handle(CreateEjemploRequestDto request, CancellationToken cancellationToken)
    {
        var listErrors = new List<string>();

        EjemploDto? result = null;

        try
        {
            result = _mapper.Map<EjemploDto>(await _EjemploRepository.Create(_mapper.Map<EjemploEntity>(request)));
        }
        catch (Exception ex)
        {
            listErrors.Add("Error Ejemplos-Ejemplo-Create");
            if (string.IsNullOrEmpty(ex.InnerException?.Message))
                listErrors.Add(ex.Message);
            else
                listErrors.Add(ex.InnerException.Message);
        }

        if (result == null)
            listErrors.Add("Error Ejemplos-Ejemplo-Null");

        return new GenericResponseDto<EjemploDto>()
        {
            Result = result,
            StatusCode = listErrors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            StatusDescription = listErrors.Count == 0 ? "Correcto" : "Error",
            Errors = listErrors.Count == 0 ? null : listErrors,
        };
    }
}