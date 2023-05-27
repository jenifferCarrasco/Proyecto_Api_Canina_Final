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

namespace APLICATION.Feauters.Vacunas.Queries.GetAllVacuna
{
    public class GetAllVacunaQuery: IRequest<PagedResponse<List<VacunasDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Nombre { get; set; }
        public string Laboratorio { get; set; }

        public class GetAllVacunaQueryHandler : IRequestHandler<GetAllVacunaQuery, PagedResponse<List<VacunasDto>>>
        {
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunas> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public GetAllVacunaQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunas> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }


            public async Task<PagedResponse<List<VacunasDto>>> Handle(GetAllVacunaQuery request, CancellationToken cancellationToken)
            {

                var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.Nombre}_{request.Laboratorio}";
                string serializedListClient;
                var listClient = new List<DOMAIN.Canina.Entities.Vacunas>();
                var redisListClient = await _distributedCache.GetAsync(cacheKey);

                if (redisListClient != null)
                {
                    serializedListClient = Encoding.UTF8.GetString(redisListClient);
                    listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Vacunas>>(serializedListClient);
                }
                else { 
                    listClient = await _repositoryAsync.ListAsync(new PagedVacunaSpecification(
                    request.PageNumber, request.PageSize, request.Nombre, request.Laboratorio));
                    serializedListClient = JsonConvert.SerializeObject(listClient);
                    redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisListClient, options);
                }


                //var client = await _repositoryAsync.ListAsync(new PagedClientSpecification(
                //    request.PageNumber, request.PageSize, request.Nombre, request.Apellido));
                var clientdto = _mapper.Map<List<VacunasDto>>(listClient); //client
                return new PagedResponse<List<VacunasDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
