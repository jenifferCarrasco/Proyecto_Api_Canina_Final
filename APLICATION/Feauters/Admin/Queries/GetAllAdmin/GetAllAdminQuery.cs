using APLICATION.DTOs;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Admin.Queries.GetAllAdmin
{
	public class GetAllAdminQuery : IRequest<PagedResponse<List<AdministradoresDto>>>
	{
        public string Email { get; set; }
        public string Username { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class GetAllAdminQueryHandler : IRequestHandler<GetAllAdminQuery, PagedResponse<List<AdministradoresDto>>>
		{
			private readonly IRepositoryAsync<Administrador> _repository;
			private readonly IMapper _mapper;

			public GetAllAdminQueryHandler(IMapper mapper, IRepositoryAsync<Administrador> repository)
			{
				_mapper = mapper;
				_repository = repository;
			}

			public async Task<PagedResponse<List<AdministradoresDto>>> Handle(GetAllAdminQuery request, CancellationToken cancellationToken)
			{
				var administradores = await _repository.ListAsync(new PagedAdminSpecification(
					request.PageSize, request.PageNumber, request.Email, request.Username));

				var propietariosDto = _mapper.Map<List<AdministradoresDto>>(administradores);

				return new PagedResponse<List<AdministradoresDto>>(propietariosDto, request.PageNumber
					, request.PageSize);
			}
		}
	}
}
