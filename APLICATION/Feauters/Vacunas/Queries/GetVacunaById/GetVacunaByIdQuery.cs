using APLICATION.DTOs;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunas.Queries.GetVacunaById
{
	public class GetVacunaByIdQuery: IRequest<Response<VacunasDto>>
    {
        public Guid Id { get; set; }
        public class GetVacunaByIdQueryHandler : IRequestHandler<GetVacunaByIdQuery, Response<VacunasDto>> {

            private readonly IRepositoryAsync<Vacuna> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetVacunaByIdQueryHandler(IRepositoryAsync<Vacuna> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<VacunasDto>> Handle(GetVacunaByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else {

                    var dto = _mapper.Map<VacunasDto>(client);
                    return new Response<VacunasDto>(dto);
                }
                
            }
        }
    }
}
