using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunadores.Commands.CreateCommand
{
	public class CreateVacunadoresCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }


    }
    public class CreateVacunadoresCommandHandler : IRequestHandler<CreateVacunadoresCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<Vacunador> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateVacunadoresCommandHandler(IRepositoryAsync<Vacunador> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateVacunadoresCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<Vacunador>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
