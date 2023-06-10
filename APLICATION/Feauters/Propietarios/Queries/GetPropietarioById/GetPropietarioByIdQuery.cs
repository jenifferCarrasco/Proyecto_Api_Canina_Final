using APLICATION.DTOs;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Propietarios.Queries.GetPropietarioById
{

	public class GetPropietarioByIdQuery : IRequest<Response<PropietariosDto>>
    {
        public Guid Id { get; set; }
        public class GetPropietarioByIdQueryHandler : IRequestHandler<GetPropietarioByIdQuery, Response<PropietariosDto>>
        {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetPropietarioByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PropietariosDto>> Handle(GetPropietarioByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    var dto = _mapper.Map<PropietariosDto>(client);
                    return new Response<PropietariosDto>(dto);
                }

            }
        }
    }
}
