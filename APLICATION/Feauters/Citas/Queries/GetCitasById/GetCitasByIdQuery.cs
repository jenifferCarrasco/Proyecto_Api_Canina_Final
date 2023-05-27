using APLICATION.DTOs;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Citas.Queries.GetCitasById
{
    public class GetCitasByIdQuery : IRequest<Response<CitasDto>>
    {
        public Guid Id { get; set; }
        public class GetCitasByIdQueryHandler : IRequestHandler<GetCitasByIdQuery, Response<CitasDto>>
        {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Citas> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCitasByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Citas> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CitasDto>> Handle(GetCitasByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    var dto = _mapper.Map<CitasDto>(client);
                    return new Response<CitasDto>(dto);
                }

            }
        }
    }
}
