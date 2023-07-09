using APLICATION.DTOs;
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

namespace APLICATION.Feauters.Admin.Queries.GetAdminById
{
	public class GetAdminByIdQuery : IRequest<Response<AdministradoresDto>>
	{
		public Guid Id { get; set; }
	}

	public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Response<AdministradoresDto>>
	{
		private readonly IRepositoryAsync<Administrador> _repositoryAsync;
		private readonly IMapper _mapper;

		public GetAdminByIdQueryHandler(IMapper mapper, IRepositoryAsync<Administrador> repositoryAsync)
		{
			_mapper = mapper;
			_repositoryAsync = repositoryAsync;
		}

		public async Task<Response<AdministradoresDto>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
		{
			var admin = (await _repositoryAsync.ListAsync(new PagedAdminSpecification(request.Id)))
				.FirstOrDefault() ??
			throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			var dto = _mapper.Map<AdministradoresDto>(admin);

			return new Response<AdministradoresDto>(dto);
		}
	}
}
