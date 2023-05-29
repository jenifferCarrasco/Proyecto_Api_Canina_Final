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

namespace APLICATION.Feauters.Vacunaciones.Queries.GetVacunacionById
{
	public class GetVacunacionByIdQuery : IRequest<Response<VacunacionesDto>>
	{
		public Guid Id { get; set; }
		public class GetVacunacionByIdQueryHandler : IRequestHandler<GetVacunacionByIdQuery, Response<VacunacionesDto>>
		{

			private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetVacunacionByIdQueryHandler(IRepositoryAsync<Vacunacion> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<VacunacionesDto>> Handle(GetVacunacionByIdQuery request, CancellationToken cancellationToken)
			{
				var vacunacion = await _repositoryAsync.GetByIdAsync(request.Id)
					?? throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

				var dto = _mapper.Map<VacunacionesDto>(vacunacion);
				return new Response<VacunacionesDto>(dto);

			}
		}
	}
}
