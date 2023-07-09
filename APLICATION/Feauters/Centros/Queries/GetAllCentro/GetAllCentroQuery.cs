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
	public class GetAllCentroQuery : IRequest<Response<List<CentrosDto>>>
	{
		public string Nombre { get; set; }
		public class GetAllCentroQueryHandler : IRequestHandler<GetAllCentroQuery, Response<List<CentrosDto>>>
		{
			private readonly IRepositoryAsync<Centro> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllCentroQueryHandler(IRepositoryAsync<Centro> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<List<CentrosDto>>> Handle(GetAllCentroQuery request, CancellationToken cancellationToken)
			{
				var centros = await _repositoryAsync.ListAsync(new PagedCentroSpecification(
					request.Nombre));

				var centrosDto = _mapper.Map<List<CentrosDto>>(centros);
				return new Response<List<CentrosDto>>(centrosDto);
			}
		}
	}
}
