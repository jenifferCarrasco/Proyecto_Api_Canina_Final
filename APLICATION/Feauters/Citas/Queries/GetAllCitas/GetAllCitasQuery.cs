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

namespace APLICATION.Feauters.Citas.Queries.GetAllCitas
{
	public class GetAllCitasQuery : IRequest<PagedResponse<List<CitasDto>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string CaninoId { get; set; }
	}
	public class GetAllCitasQueryHandler : IRequestHandler<GetAllCitasQuery, PagedResponse<List<CitasDto>>>
	{
		private readonly IRepositoryAsync<Cita> _repositoryAsync;
		private readonly IMapper _mapper;

		public GetAllCitasQueryHandler(IRepositoryAsync<Cita> repositoryAsync, IMapper mapper)
		{
			_repositoryAsync = repositoryAsync;
			_mapper = mapper;
		}


		public async Task<PagedResponse<List<CitasDto>>> Handle(GetAllCitasQuery request, CancellationToken cancellationToken)
		{

			var citas = await _repositoryAsync.ListAsync(new PagedCitaSpecification(
			request.PageNumber, request.PageSize, request.CaninoId));

			var citasDto = _mapper.Map<List<CitasDto>>(citas);
			return new PagedResponse<List<CitasDto>>(citasDto, request.PageNumber
				, request.PageSize);
		}
	}
}
