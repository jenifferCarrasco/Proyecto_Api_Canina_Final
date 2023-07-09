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

namespace APLICATION.Feauters.Vacunas.Queries.GetAllVacuna
{
	public class GetAllVacunaQuery : IRequest<Response<List<VacunasDto>>>
	{
		public string Nombre { get; set; }

		public class GetAllVacunaQueryHandler : IRequestHandler<GetAllVacunaQuery, Response<List<VacunasDto>>>
		{
			private readonly IRepositoryAsync<Vacuna> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllVacunaQueryHandler(IRepositoryAsync<Vacuna> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<Response<List<VacunasDto>>> Handle(GetAllVacunaQuery request, CancellationToken cancellationToken)
			{
				var vacunas = await _repositoryAsync.ListAsync(
					new PagedVacunaSpecification(request.Nombre));

				var vacunasDto = _mapper.Map<List<VacunasDto>>(vacunas);
				return new Response<List<VacunasDto>>(vacunasDto);
			}
		}
	}
}
