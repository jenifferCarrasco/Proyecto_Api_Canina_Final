using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.UpdateVacunacionCommand
{
	public class UpdateVacunacionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CentroId { get; set; }
        public Guid VacunadorId { get; set; }
        public Guid VacunaId { get; set; }
        public Guid CaninoId { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }

    }
    public class UpdateVacunacionCommandHandler : IRequestHandler<UpdateVacunacionCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
        public UpdateVacunacionCommandHandler(IRepositoryAsync<Vacunacion> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
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
