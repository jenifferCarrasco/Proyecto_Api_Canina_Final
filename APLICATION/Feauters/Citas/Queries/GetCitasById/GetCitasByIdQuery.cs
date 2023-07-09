﻿using APLICATION.DTOs;
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

namespace APLICATION.Feauters.Citas.Queries.GetCitasById
{
	public class GetCitasByIdQuery : IRequest<Response<CitasDto>>
	{
		public Guid Id { get; set; }
		public class GetCitasByIdQueryHandler : IRequestHandler<GetCitasByIdQuery, Response<CitasDto>>
		{

			private readonly IRepositoryAsync<Cita> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetCitasByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Cita> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<CitasDto>> Handle(GetCitasByIdQuery request, CancellationToken cancellationToken)
			{
				var cita = (await _repositoryAsync.ListAsync(new PagedCitaSpecification(request.Id))).FirstOrDefault() ??
					throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

				var dto = _mapper.Map<CitasDto>(cita);
				return new Response<CitasDto>(dto);
			}
		}
	}
}
