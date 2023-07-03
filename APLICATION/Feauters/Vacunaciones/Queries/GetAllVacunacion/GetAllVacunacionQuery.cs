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

namespace APLICATION.Feauters.Vacunaciones.Queries.GetAllVacunacion
{
	public class GetAllVacunacionQuery : IRequest<PagedResponse<List<VacunacionesDto>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public Guid CaninoId { get; set; }
		public Guid VacunadorId { get; set; }

		public class GetAllVacunacionQueryHandler : IRequestHandler<GetAllVacunacionQuery, PagedResponse<List<VacunacionesDto>>>
		{
			private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllVacunacionQueryHandler(IRepositoryAsync<Vacunacion> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}


			public async Task<PagedResponse<List<VacunacionesDto>>> Handle(GetAllVacunacionQuery request, CancellationToken cancellationToken)
			{

				var vacunaciones = await _repositoryAsync.ListAsync(new PagedVacunacionSpecification(
				request.PageSize, request.PageNumber, request.CaninoId, request.VacunadorId));

				var vacunacionesDto = _mapper.Map<List<VacunacionesDto>>(vacunaciones);
				return new PagedResponse<List<VacunacionesDto>>(vacunacionesDto, request.PageNumber
					, request.PageSize);
			}
		}
	}
}
