using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Citas.Commands.UpdateCitasCommand
{
	public class UpdateCitasCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CentroId { get; set; }
        public Guid VacunadorId { get; set; }
        public Guid CaninoId { get; set; }
        public Estados Estatus { get; set; }
        public DateTime FechaCita { get; set; }
    }
    public class UpdateCitasCommandHandler : IRequestHandler<UpdateCitasCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Cita> _repositoryAsync;

        public UpdateCitasCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Cita> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(UpdateCitasCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                client.FechaCita = request.FechaCita;
                client.VacunadorId = request.VacunadorId;
                client.CaninoId = request.CaninoId;
                client.CentroId = request.CentroId;
                client.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
