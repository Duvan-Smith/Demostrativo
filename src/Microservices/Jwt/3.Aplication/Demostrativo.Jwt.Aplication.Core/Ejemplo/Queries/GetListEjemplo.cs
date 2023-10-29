using AutoMapper;
using Demostrativo.Jwt.Aplication.Dto.Base;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo;
using Demostrativo.Jwt.Aplication.Dto.Ejemplo.Queries;
using Demostrativo.Jwt.Domain.Ejemplo;
using MediatR;
using System.Net;

namespace Demostrativo.Jwt.Aplication.Core.Ejemploes.Queries;

public class GetListEjemplo : IRequestHandler<GetListEjemploRequestDto, GenericResponseDto<List<EjemploDto>>>
{
    private readonly IMapper _mapper;
    private readonly IEjemploRepository _EjemploRepository;

    public GetListEjemplo(IMapper mapper, IEjemploRepository EjemploRepository)
    {
        _mapper = mapper;
        _EjemploRepository = EjemploRepository;
    }

    public async Task<GenericResponseDto<List<EjemploDto>>> Handle(GetListEjemploRequestDto request, CancellationToken cancellationToken)
    {
        var listErrors = new List<string>();

        List<EjemploDto>? result = null;

        try
        {
            result = _mapper.Map<List<EjemploDto>>(await _EjemploRepository.GetAll());
        }
        catch (Exception ex)
        {
            listErrors.Add("Error Ejemplos-Ejemplo-GetAll");
            if (string.IsNullOrEmpty(ex.InnerException?.Message))
                listErrors.Add(ex.Message);
            else
                listErrors.Add(ex.InnerException.Message);
        }

        if (result == null)
            listErrors.Add("Error consulta");

        return new GenericResponseDto<List<EjemploDto>>()
        {
            Result = result,
            StatusCode = listErrors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            StatusDescription = listErrors.Count == 0 ? "Correcto" : "Error",
            Errors = listErrors.Count == 0 ? null : listErrors,
        };
    }
}