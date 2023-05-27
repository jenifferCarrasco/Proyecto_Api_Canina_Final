using APLICATION.DTOs;
using Application.Interface;
using APLICATION.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Queries.GetVacunacionById
{
    public class GetVacunacionByIdQuery: IRequest<Response<VacunacionesDto>>
    {
        public Guid Id { get; set; }
        public class GetVacunacionByIdQueryHandler : IRequestHandler<GetVacunacionByIdQuery, Response<VacunacionesDto>> {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetVacunacionByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<VacunacionesDto>> Handle(GetVacunacionByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else {

                    var dto = _mapper.Map<VacunacionesDto>(client);
                    return new Response<VacunacionesDto>(dto);
                }
                
            }
        }
    }
}
