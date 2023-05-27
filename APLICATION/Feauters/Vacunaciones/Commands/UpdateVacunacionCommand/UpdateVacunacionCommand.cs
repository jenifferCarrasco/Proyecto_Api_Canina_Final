using APLICATION.Exceptions;
using Application.Interface;
using APLICATION.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Vacunaciones.Commands.UpdateVacunacionCommand
{
    
    public class UpdateVacunacionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
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
    public class UpdateVacunacionCommandHandler : IRequestHandler<UpdateVacunacionCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateVacunacionCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunaciones> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdateVacunacionCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else {

                client.CentroId = request.CentroId;
                client.CaninoId = request.CaninoId;
                client.VacunaId = request.VacunaId;
                client.VacunadorId = request.VacunadorId;
                client.Dosis = request.Dosis;
                client.FechaProxima = request.FechaProxima;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
