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


namespace APLICATION.Feauters.Vacunadores.Queries.GetAllVacunadores
{

	public class GetAllVacunadoresQuery : IRequest<PagedResponse<List<VacunadoresDto>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string Nombre { get; set; }
		public string Cedula { get; set; }

		public class GetAllVacunadoresQueryHandler : IRequestHandler<GetAllVacunadoresQuery, PagedResponse<List<VacunadoresDto>>>
		{
			private readonly IRepositoryAsync<Vacunador> _repositoryAsync;
			private readonly IMapper _mapper;

			public GetAllVacunadoresQueryHandler(IRepositoryAsync<Vacunador> repositoryAsync, IMapper mapper)
			{
				_repositoryAsync = repositoryAsync;
				_mapper = mapper;
			}

			public async Task<PagedResponse<List<VacunadoresDto>>> Handle(GetAllVacunadoresQuery request, CancellationToken cancellationToken)
			{

				var vacunadores = await _repositoryAsync.ListAsync(new PagedVacunadoresSpecification(
				request.PageNumber, request.PageSize, request.Nombre, request.Cedula));

				var vacunadoresDto = _mapper.Map<List<VacunadoresDto>>(vacunadores);
				return new PagedResponse<List<VacunadoresDto>>(vacunadoresDto, request.PageNumber
					, request.PageSize);
			}
		}
	}
}
