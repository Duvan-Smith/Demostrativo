using AutoMapper;
using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Commands;
using Demostrativo.Jwt.Domain.Ejemplo;
using MediatR;
using System.Net;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes.Commands;

public class UpdateEjemplo : IRequestHandler<UpdateEjemploRequestDto, GenericResponseDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IEjemploRepository _EjemploRepository;

    public UpdateEjemplo(IMapper mapper, IEjemploRepository EjemploRepository)
    {
        _mapper = mapper;
        _EjemploRepository = EjemploRepository;
    }

    public async Task<GenericResponseDto<bool>> Handle(UpdateEjemploRequestDto request, CancellationToken cancellationToken)
    {
        var listErrors = new List<string>();

        var result = false;

        try
        {
            result = await _EjemploRepository.Update(_mapper.Map<EjemploEntity>(request));
        }
        catch (Exception ex)
        {
            listErrors.Add("Error Ejemplos-Ejemplo-Update");
            if (string.IsNullOrEmpty(ex.InnerException?.Message))
                listErrors.Add(ex.Message);
            else
                listErrors.Add(ex.InnerException.Message);
        }

        if (!result)
            listErrors.Add("Error Ejemplos-Ejemplo-Update-false");

        return new GenericResponseDto<bool>()
        {
            Result = result,
            StatusCode = listErrors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            StatusDescription = listErrors.Count == 0 ? "Correcto" : "Error",
            Errors = listErrors.Count == 0 ? null : listErrors,
        };
    }
}