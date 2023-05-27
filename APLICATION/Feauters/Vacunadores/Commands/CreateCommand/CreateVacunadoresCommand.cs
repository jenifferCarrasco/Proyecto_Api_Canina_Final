using Application.Interface;
using APLICATION.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Vacunadores.Commands.CreateCommand
{
    public class CreateVacunadoresCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string UsuarioId { get; set; }


    }
    public class CreateVacunadoresCommandHandler : IRequestHandler<CreateVacunadoresCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunadores> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateVacunadoresCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunadores> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateVacunadoresCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Vacunadores>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
