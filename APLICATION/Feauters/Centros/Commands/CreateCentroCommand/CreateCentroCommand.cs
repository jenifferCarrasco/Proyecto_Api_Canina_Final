using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Centros.Commands.CreateCentroCommand
{
  
    public class CreateCentroCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Estados Estatus { get; set; }

    }
    public class CreateCentroCommandHandler : IRequestHandler<CreateCentroCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centros> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCentroCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centros> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateCentroCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Centros>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
