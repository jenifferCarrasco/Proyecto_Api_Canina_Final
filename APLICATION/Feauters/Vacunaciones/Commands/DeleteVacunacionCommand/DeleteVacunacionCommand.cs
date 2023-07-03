using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.DeleteVacunacionCommand
{

	public class DeleteVacunacionCommand : IRequest<Response<Guid>>
	{
		public Guid Id { get; set; }
	}
	public class DeleteVacunacionCommandHandler : IRequestHandler<DeleteVacunacionCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<Vacunacion> _repositoryAsync;
		public DeleteVacunacionCommandHandler(IRepositoryAsync<Vacunacion> repositoryAsync)
		{
			_repositoryAsync = repositoryAsync;

		}

		public async Task<Response<Guid>> Handle(DeleteVacunacionCommand request, CancellationToken cancellationToken)
		{
			var vacunacion = await _repositoryAsync.GetByIdAsync(request.Id);
			if (vacunacion == null)
			{
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
			}
			else
			{
				await _repositoryAsync.DeleteAsync(vacunacion);
				return new Response<Guid>(vacunacion.Id);
			}
		}
	}
}
