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

namespace APLICATION.Feauters.Centros.Queries.GetCentroById
{

	public class GetCentroByIdQuery : IRequest<Response<CentrosDto>>
	{
		public Guid Id { get; set; }
		public class GetCentroByIdQueryHandler : IRequestHandler<GetCentroByIdQuery, Response<CentrosDto>>
		{

			private readonly IRepositoryAsync<Centro> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetCentroByIdQueryHandler(IRepositoryAsync<Centro> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<CentrosDto>> Handle(GetCentroByIdQuery request, CancellationToken cancellationToken)
			{
				var centro = await _repositoryAsync.GetByIdAsync(request.Id)
					?? throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

				var dto = _mapper.Map<CentrosDto>(centro);
				return new Response<CentrosDto>(dto);
			}
		}
	}
}
