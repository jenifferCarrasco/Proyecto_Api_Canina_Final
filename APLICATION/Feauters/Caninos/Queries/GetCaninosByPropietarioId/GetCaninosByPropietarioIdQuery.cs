using APLICATION.DTOs;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Caninos.Queries.GetCaninosByPropietarioId
{
	public class GetCaninosByPropietarioIdQuery : IRequest<Response<List<CaninoDto>>>
	{
		public Guid PropietarioId { get; set; }


		public class GetCaninosByPropietarioIdQueryHandler : IRequestHandler<GetCaninosByPropietarioIdQuery, Response<List<CaninoDto>>>
		{
			private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetCaninosByPropietarioIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}


			public async Task<Response<List<CaninoDto>>> Handle(GetCaninosByPropietarioIdQuery request, CancellationToken cancellationToken)
			{
				var caninos = await _repositoryAsync.ListAsync(new CaninoSpecification(request.PropietarioId));

				if (caninos.Count < 1)
				{
					throw new KeyNotFoundException("No se encontraron caninos para este propietario");
				}

				var caninosDto = _mapper.Map<List<CaninoDto>>(caninos);

				return new Response<List<CaninoDto>>(caninosDto);
			}
		}
	}
}
