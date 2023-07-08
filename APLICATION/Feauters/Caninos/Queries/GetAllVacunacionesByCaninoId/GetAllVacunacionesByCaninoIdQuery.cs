using APLICATION.DTOs;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Caninos.Queries.GetAllVacunacionesByCaninoId
{
	public class GetAllVacunacionesByCaninoIdQuery : IRequest<Response<List<VacunacionesDto>>>
	{
		public Guid CaninoId { get; set; }

		public class GetAllVacunacionesByCaninoIdQueryHandler : IRequestHandler<GetAllVacunacionesByCaninoIdQuery, Response<List<VacunacionesDto>>>
		{
			private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllVacunacionesByCaninoIdQueryHandler(IMapper mapper, IRepositoryAsync<Vacunacion> repositoryAsync)
			{
				_mapper = mapper;
				_repositoryAsync = repositoryAsync;
			}

			public async Task<Response<List<VacunacionesDto>>> Handle(GetAllVacunacionesByCaninoIdQuery request, CancellationToken cancellationToken)
			{
				var vacunaciones = await _repositoryAsync.ListAsync(
					new PagedVacunacionSpecification(request.CaninoId));

				var vacunacionesDto = _mapper.Map<List<VacunacionesDto>>(vacunaciones);

				return new Response<List<VacunacionesDto>>(vacunacionesDto);
			}
		}
	}
}
