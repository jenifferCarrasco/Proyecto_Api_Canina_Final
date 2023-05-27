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

namespace APLICATION.Feauters.Citas.Queries.GetAllCitas
{
    public class GetAllCitasQuery : IRequest<PagedResponse<List<CitasDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DateTime FechaCita { get; set; }
        public Guid CaninoId { get; set; }
    }
    public class GetAllCitasQueryHandler : IRequestHandler<GetAllCitasQuery, PagedResponse<List<CitasDto>>>
    {
        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Citas> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetAllCitasQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Citas> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }


        public async Task<PagedResponse<List<CitasDto>>> Handle(GetAllCitasQuery request, CancellationToken cancellationToken)
        {

            var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.FechaCita}_{request.CaninoId}";
            string serializedListClient;
            var listClient = new List<DOMAIN.Canina.Entities.Citas>();
            var redisListClient = await _distributedCache.GetAsync(cacheKey);

            if (redisListClient != null)
            {
                serializedListClient = Encoding.UTF8.GetString(redisListClient);
                listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Citas>>(serializedListClient);
            }
            else
            {
                listClient = await _repositoryAsync.ListAsync(new PagedCitaSpecification(
                request.PageNumber, request.PageSize, request.FechaCita, request.CaninoId));
                serializedListClient = JsonConvert.SerializeObject(listClient);
                redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetAsync(cacheKey, redisListClient, options);
            }


            //var client = await _repositoryAsync.ListAsync(new PagedClientSpecification(
            //    request.PageNumber, request.PageSize, request.FechaCita, request.CaninoId));
            var clientdto = _mapper.Map<List<CitasDto>>(listClient); //client
            return new PagedResponse<List<CitasDto>>(clientdto, request.PageNumber
                , request.PageSize);
        }
    }
}
