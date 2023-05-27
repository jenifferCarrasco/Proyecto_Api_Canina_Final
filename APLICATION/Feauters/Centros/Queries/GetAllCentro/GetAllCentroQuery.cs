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

namespace APLICATION.Feauters.Centros.Queries.GetAllCentro
{
    
    public class GetAllCentroQuery : IRequest<PagedResponse<List<CentrosDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Nombre { get; set; }


        public class GetAllCentroQueryHandler : IRequestHandler<GetAllCentroQuery, PagedResponse<List<CentrosDto>>>
        {
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centros> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public GetAllCentroQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centros> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }


            public async Task<PagedResponse<List<CentrosDto>>> Handle(GetAllCentroQuery request, CancellationToken cancellationToken)
            {

                var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.Nombre}";
                string serializedListClient;
                var listClient = new List<DOMAIN.Canina.Entities.Centros>();
                var redisListClient = await _distributedCache.GetAsync(cacheKey);

                if (redisListClient != null)
                {
                    serializedListClient = Encoding.UTF8.GetString(redisListClient);
                    listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Centros>>(serializedListClient);
                }
                else
                {
                    listClient = await _repositoryAsync.ListAsync(new PagedCentroSpecification(
                    request.PageNumber, request.PageSize, request.Nombre));
                    serializedListClient = JsonConvert.SerializeObject(listClient);
                    redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisListClient, options);
                }


                //var client = await _repositoryAsync.ListAsync(new PagedCentroSpecification(
                //    request.PageNumber, request.PageSize, request.Nombre));
                var clientdto = _mapper.Map<List<CentrosDto>>(listClient); //client
                return new PagedResponse<List<CentrosDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
