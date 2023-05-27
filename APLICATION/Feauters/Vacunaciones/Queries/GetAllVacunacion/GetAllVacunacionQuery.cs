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

namespace APLICATION.Feauters.Vacunaciones.Queries.GetAllVacunacion
{
    public class GetAllVacunacionQuery: IRequest<PagedResponse<List<VacunacionesDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid CaninoId { get; set; }
        public Guid VacunadorId { get; set; }

        public class GetAllVacunacionQueryHandler : IRequestHandler<GetAllVacunacionQuery, PagedResponse<List<VacunacionesDto>>>
        {
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public GetAllVacunacionQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }


            public async Task<PagedResponse<List<VacunacionesDto>>> Handle(GetAllVacunacionQuery request, CancellationToken cancellationToken)
            {

                var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.CaninoId}_{request.VacunadorId}";
                string serializedListClient;
                var listClient = new List<DOMAIN.Canina.Entities.Vacunaciones>();
                var redisListClient = await _distributedCache.GetAsync(cacheKey);

                if (redisListClient != null)
                {
                    serializedListClient = Encoding.UTF8.GetString(redisListClient);
                    listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Vacunaciones>>(serializedListClient);
                }
                else { 
                    listClient = await _repositoryAsync.ListAsync(new PagedVacunacionSpecification(
                    request.PageNumber, request.PageSize, request.CaninoId, request.VacunadorId));
                    serializedListClient = JsonConvert.SerializeObject(listClient);
                    redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisListClient, options);
                }


                //var client = await _repositoryAsync.ListAsync(new PagedClientSpecification(
                //    request.PageNumber, request.PageSize, request.Nombre, request.Apellido));
                var clientdto = _mapper.Map<List<VacunacionesDto>>(listClient); //client
                return new PagedResponse<List<VacunacionesDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
