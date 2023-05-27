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
            private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunadores> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public GetAllVacunadoresQueryHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunadores> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }


            public async Task<PagedResponse<List<VacunadoresDto>>> Handle(GetAllVacunadoresQuery request, CancellationToken cancellationToken)
            {

                var cacheKey = $"ListClient_{request.PageNumber}_{request.PageSize}_{request.Nombre}_{request.Cedula}";
                string serializedListClient;
                var listClient = new List<DOMAIN.Canina.Entities.Vacunadores>();
                var redisListClient = await _distributedCache.GetAsync(cacheKey);

                if (redisListClient != null)
                {
                    serializedListClient = Encoding.UTF8.GetString(redisListClient);
                    listClient = JsonConvert.DeserializeObject<List<DOMAIN.Canina.Entities.Vacunadores>>(serializedListClient);
                }
                else
                {
                    listClient = await _repositoryAsync.ListAsync(new PagedVacunadoresSpecification(
                    request.PageNumber, request.PageSize, request.Nombre, request.Cedula));
                    serializedListClient = JsonConvert.SerializeObject(listClient);
                    redisListClient = Encoding.UTF8.GetBytes(serializedListClient);


                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey, redisListClient, options);
                }


                //var client = await _repositoryAsync.ListAsync(new PagedCaninoSpecification(
                //    request.PageNumber, request.PageSize, request.Nombre, request.Raza));
                var clientdto = _mapper.Map<List<VacunadoresDto>>(listClient); //client
                return new PagedResponse<List<VacunadoresDto>>(clientdto, request.PageNumber
                    , request.PageSize);
            }
        }
    }
}
