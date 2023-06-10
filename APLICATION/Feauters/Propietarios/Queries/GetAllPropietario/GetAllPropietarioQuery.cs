using APLICATION.DTOs;
using APLICATION.Specification;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Propietarios.Queries.GetAllPropietario
{

    public class GetAllPropietarioQuery : IRequest<PagedResponse<List<PropietariosDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }

        public class GetAllPropietarioQueryHandler : IRequestHandler<GetAllPropietarioQuery, PagedResponse<List<PropietariosDto>>>
        {
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllPropietarioQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }


            public async Task<PagedResponse<List<PropietariosDto>>> Handle(GetAllPropietarioQuery request, CancellationToken cancellationToken)
            {

                var propietarios = await _repositoryAsync.ListAsync(new PagedPropietarioSpecification(
                    request.PageSize, request.PageNumber, request.Nombre, request.Cedula));

                var propietariosDto = _mapper.Map<List<PropietariosDto>>(propietarios);

                return new PagedResponse<List<PropietariosDto>>(propietariosDto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
