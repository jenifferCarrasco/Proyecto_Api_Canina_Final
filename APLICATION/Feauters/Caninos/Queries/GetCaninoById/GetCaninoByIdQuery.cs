using APLICATION.DTOs;
using Application.Interface;
using APLICATION.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Caninos.Queries.GetCaninoById
{
    public class GetCaninoByIdQuery: IRequest<Response<CaninoDto>>
    {
        public Guid Id { get; set; }
        public class GetCaninoByIdQueryHandler : IRequestHandler<GetCaninoByIdQuery, Response<CaninoDto>> {

            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCaninoByIdQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CaninoDto>> Handle(GetCaninoByIdQuery request, CancellationToken cancellationToken)
            {
                var client = await _repositoryAsync.GetByIdAsync(request.Id);
                if (client == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else {

                    var dto = _mapper.Map<CaninoDto>(client);
                    return new Response<CaninoDto>(dto);
                }
                
            }
        }
    }
}
