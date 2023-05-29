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
        public Guid CentroId { get; set; }
        public Centro Centro { get; set; }
        public Guid VacunadorId { get; set; }
        public Vacunador Vacunador { get; set; }
        public Guid VacunaId { get; set; }
        public Vacuna Vacuna { get; set; }
        public Guid CaninoId { get; set; }
        public DOMAIN.Canina.Entities.Canino Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }

    }
    public class UpdateVacunacionCommandHandler : IRequestHandler<UpdateVacunacionCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunacion> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateVacunacionCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunacion> repositoryAsync, IMapper mapper = null)
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
