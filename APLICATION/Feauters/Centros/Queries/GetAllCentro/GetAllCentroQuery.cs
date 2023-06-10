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

namespace APLICATION.Feauters.Centros.Queries.GetAllCentro
{

	public class GetAllCentroQuery : IRequest<PagedResponse<List<CentrosDto>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string Nombre { get; set; }


		public class GetAllCentroQueryHandler : IRequestHandler<GetAllCentroQuery, PagedResponse<List<CentrosDto>>>
		{
			private readonly IRepositoryAsync<Centro> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllCentroQueryHandler(IRepositoryAsync<Centro> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}


			public async Task<PagedResponse<List<CentrosDto>>> Handle(GetAllCentroQuery request, CancellationToken cancellationToken)
			{

				var centros = await _repositoryAsync.ListAsync(new PagedCentroSpecification(
					request.PageSize,
					request.PageNumber,
					request.Nombre));

				var clientdto = _mapper.Map<List<CentrosDto>>(centros);
				return new PagedResponse<List<CentrosDto>>(clientdto, request.PageNumber, request.PageSize);
			}
		}
	}
}
