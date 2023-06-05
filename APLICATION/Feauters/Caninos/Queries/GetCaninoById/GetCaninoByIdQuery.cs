using APLICATION.DTOs;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Caninos.Queries.GetCaninoById
{
	public class GetCaninoByIdQuery : IRequest<Response<CaninoDto>>
	{
		public Guid Id { get; set; }
		public class GetCaninoByIdQueryHandler : IRequestHandler<GetCaninoByIdQuery, Response<CaninoDto>>
		{

			private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetCaninoByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<CaninoDto>> Handle(GetCaninoByIdQuery request, CancellationToken cancellationToken)
			{
				var canino = await _repositoryAsync.GetByIdAsync(request.Id) ??
					throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");


				var dto = _mapper.Map<CaninoDto>(canino);

				return new Response<CaninoDto>(dto);
			}
		}
	}
}
