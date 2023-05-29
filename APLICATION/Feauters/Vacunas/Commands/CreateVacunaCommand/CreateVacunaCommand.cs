using Application.Interface;
using APLICATION.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;

namespace APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand
{
    public class CreateVacunaCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public string Lote { get; set; }
        public DOMAIN.Canina.Estados Estatus { get; set; }


    }
    public class CreateVacunaCommandHandler : IRequestHandler<CreateVacunaCommand, Response<Guid>> {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateVacunaCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateVacunaCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Vacuna>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
