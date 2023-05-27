using Application.Interface;
using APLICATION.Wrappers;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand
{
    public class CreateVacunacionCommand : IRequest<Response<Guid>>
    {
        public string CentroId { get; set; }
        public DOMAIN.Canina.Entities.Centros Centro { get; set; }
        public string VacunadorId { get; set; }
        public DOMAIN.Canina.Entities.Vacunadores Vacunador { get; set; }
        public string VacunaId { get; set; }
        public DOMAIN.Canina.Entities.Vacunas Vacuna { get; set; }
        public string CaninoId { get; set; }
        public DOMAIN.Canina.Entities.Caninos Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }

    }
    public class CreateVacunacionCommandHandler : IRequestHandler<CreateVacunacionCommand, Response<Guid>> {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateVacunacionCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateVacunacionCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Vacunaciones>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
