using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand
{
	public class CreateVacunacionCommand : IRequest<Response<Guid>>
    {
        public string CentroId { get; set; }
        public string VacunadorId { get; set; }
        public string VacunaId { get; set; }
        public string CaninoId { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }

    }
    public class CreateVacunacionCommandHandler : IRequestHandler<CreateVacunacionCommand, Response<Guid>> {

        private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateVacunacionCommandHandler(IRepositoryAsync<Vacunacion> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateVacunacionCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<Vacunacion>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
