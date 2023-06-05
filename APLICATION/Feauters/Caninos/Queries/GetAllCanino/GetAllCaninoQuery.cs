using APLICATION.DTOs;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Canino.Queries.GetAllCanino
{
	public class GetAllCaninoQuery: IRequest<PagedResponse<List<CaninoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }

        public class GetAllCaninoQueryHandler : IRequestHandler<GetAllCaninoQuery, PagedResponse<List<CaninoDto>>>
        {
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCaninoQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }


            public async Task<PagedResponse<List<CaninoDto>>> Handle(GetAllCaninoQuery request, CancellationToken cancellationToken)
            {

                var client = await _repositoryAsync.ListAsync(new CaninoSpecification(
                    request.PageSize, request.PageNumber, request.Nombre, request.Raza));

				var clientdto = _mapper.Map<List<CaninoDto>>(client);

                return new PagedResponse<List<CaninoDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
