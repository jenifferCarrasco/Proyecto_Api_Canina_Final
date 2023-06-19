using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Citas.Commands.DesactivarCitaCommand
{
	public class DesactivarCitaCommand : IRequest<Response<string>>
	{
        public Guid Id { get; set; }
    }

	public class DesactivarCitaCommandHandler : IRequestHandler<DesactivarCitaCommand, Response<string>>
	{
		private readonly IRepositoryAsync<Cita> _repositoryAsync;

		public DesactivarCitaCommandHandler(IRepositoryAsync<Cita> repositoryAsync)
		{
			_repositoryAsync = repositoryAsync;
		}

		public async Task<Response<string>> Handle(DesactivarCitaCommand request, CancellationToken cancellationToken)
		{
			var cita = await _repositoryAsync.GetByIdAsync(request.Id) ??
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			cita.Estatus = Estados.Inactivo;

			await _repositoryAsync.UpdateAsync(cita);

			return new Response<string>(cita.Id.ToString(), "La cita ha sido Inhabilitada");
		}
	}
}
