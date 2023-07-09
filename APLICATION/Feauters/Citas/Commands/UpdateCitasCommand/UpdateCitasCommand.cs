using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
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
        public DateTime FechaCita { get; set; }
    }
    public class UpdateCitasCommandHandler : IRequestHandler<UpdateCitasCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<Cita> _repositoryAsync;

        public UpdateCitasCommandHandler(IRepositoryAsync<Cita> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Guid>> Handle(UpdateCitasCommand request, CancellationToken cancellationToken)
        {
            var cita = await _repositoryAsync.GetByIdAsync(request.Id);
            if (cita == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                cita.FechaCita = request.FechaCita;
                cita.VacunadorId = request.VacunadorId;
                cita.CaninoId = request.CaninoId;
                cita.CentroId = request.CentroId;

                await _repositoryAsync.UpdateAsync(cita);

                return new Response<Guid>(cita.Id);
            }
        }
    }
}
