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

namespace APLICATION.Feauters.Vacunadores.Queries.GetVacunadoresById
{

    public class GetVacunadoresByIdQuery : IRequest<Response<VacunadoresDto>>
    {
        public Guid Id { get; set; }
        public class GetVacunadoresByIdQueryHandler : IRequestHandler<GetVacunadoresByIdQuery, Response<VacunadoresDto>>
        {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunador> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetVacunadoresByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunador> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<VacunadoresDto>> Handle(GetVacunadoresByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    var dto = _mapper.Map<VacunadoresDto>(client);
                    return new Response<VacunadoresDto>(dto);
                }

            }
        }
    }
}
