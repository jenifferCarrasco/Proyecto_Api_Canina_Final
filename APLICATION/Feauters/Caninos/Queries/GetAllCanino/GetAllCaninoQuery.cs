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
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public GetAllCaninoQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }


            public async Task<PagedResponse<List<CaninoDto>>> Handle(GetAllCaninoQuery request, CancellationToken cancellationToken)
            {

                var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.Nombre}_{request.Raza}";
                string serializedListClient;
                var listClient = new List<DOMAIN.Canina.Entities.Caninos>();
                var redisListClient = await _distributedCache.GetAsync(cacheKey);

                if (redisListClient != null)
                {
                    serializedListClient = Encoding.UTF8.GetString(redisListClient);
                    listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Caninos>>(serializedListClient);
                }
                else { 
                    listClient = await _repositoryAsync.ListAsync(new PagedCaninoSpecification(
                    request.PageNumber, request.PageSize, request.Nombre, request.Raza));
                    serializedListClient = JsonConvert.SerializeObject(listClient);
                    redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisListClient, options);
                }


                //var client = await _repositoryAsync.ListAsync(new PagedCaninoSpecification(
                //    request.PageNumber, request.PageSize, request.Nombre, request.Raza));
                var clientdto = _mapper.Map<List<CaninoDto>>(listClient); //client
                return new PagedResponse<List<CaninoDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
