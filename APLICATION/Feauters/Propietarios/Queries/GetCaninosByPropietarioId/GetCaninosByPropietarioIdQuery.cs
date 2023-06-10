using APLICATION.DTOs;
using APLICATION.Exceptions;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Propietarios.Queries.GetCaninosByPropietarioId
{
	public class GetCaninosByPropietarioIdQuery : IRequest<Response<List<CaninoDto>>>
	{
		public Guid PropietarioId { get; set; }
	}
	public class GetCaninosByPropietarioIdQueryHandler : IRequestHandler<GetCaninosByPropietarioIdQuery,Response<List<CaninoDto>>>
	{
		private readonly IRepositoryAsync<Propietario> _repositoryAsync;
		private readonly IMapper _mapper;

		public GetCaninosByPropietarioIdQueryHandler(IRepositoryAsync<Propietario> repositoryAsync, IMapper mapper)
		{
			_repositoryAsync = repositoryAsync;
			_mapper = mapper;
		}

		public async Task<Response<List<CaninoDto>>> Handle(GetCaninosByPropietarioIdQuery request, CancellationToken cancellationToken)
		{
			var propietario = (await _repositoryAsync.ListAsync(new PagedPropietarioSpecification(request.PropietarioId)))
				.FirstOrDefault() ?? throw new ApiException("Este Propietario no existe");

			var caninosDto = _mapper.Map<List<CaninoDto>>(propietario.Caninos);

			return new Response<List<CaninoDto>>(caninosDto);
		}
	}
}
