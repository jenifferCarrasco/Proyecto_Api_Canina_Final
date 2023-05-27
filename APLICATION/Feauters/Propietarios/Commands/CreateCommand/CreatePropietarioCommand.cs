using Application.Interface;
using APLICATION.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Propietarios.Commands.CreateCommand
{
    public class CreatePropietarioCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string UsuarioId { get; set; }


    }
    public class CreatePropietarioCommandHandler : IRequestHandler<CreatePropietarioCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Propietarios> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreatePropietarioCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Propietarios> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreatePropietarioCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Propietarios>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
