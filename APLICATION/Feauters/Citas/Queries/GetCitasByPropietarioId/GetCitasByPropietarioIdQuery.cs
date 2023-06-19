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

namespace APLICATION.Feauters.Citas.Queries.GetCitasByPropietarioId
{
	public class GetCitasByPropietarioIdQuery : IRequest<Response<List<CitasDto>>>
	{
        public Guid PropietarioId { get; set; }
    }

	public class GetCitasByPropietarioIdQueryHandler : IRequestHandler<GetCitasByPropietarioIdQuery, Response<List<CitasDto>>>
	{
		private readonly IRepositoryAsync<Cita> _repositoryAsync;
		private readonly IMapper _mapper;

		public GetCitasByPropietarioIdQueryHandler(IRepositoryAsync<Cita> repositoryAsync, IMapper mapper)
		{
			_repositoryAsync = repositoryAsync;
			_mapper = mapper;
		}
		public async Task<Response<List<CitasDto>>> Handle(GetCitasByPropietarioIdQuery request, CancellationToken cancellationToken)
		{
			var citas = await _repositoryAsync.ListAsync(new PagedCitaSpecification(request.PropietarioId, null));

			var citasDto = _mapper.Map<List<CitasDto>>(citas);
			return new Response<List<CitasDto>>(citasDto);
		}
	}

}
