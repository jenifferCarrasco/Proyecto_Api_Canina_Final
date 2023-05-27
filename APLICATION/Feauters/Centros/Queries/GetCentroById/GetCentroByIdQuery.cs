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

namespace APLICATION.Feauters.Centros.Queries.GetCentroById
{

    public class GetCentroByIdQuery : IRequest<Response<CentrosDto>>
    {
        public Guid Id { get; set; }
        public class GetCentroByIdQueryHandler : IRequestHandler<GetCentroByIdQuery, Response<CentrosDto>>
        {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centros> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCentroByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centros> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CentrosDto>> Handle(GetCentroByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    var dto = _mapper.Map<CentrosDto>(client);
                    return new Response<CentrosDto>(dto);
                }

            }

        }
    }

}
