using APLICATION.Exceptions;
using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunas.Commands.UpdateInventarioCommand
{
	public class UpdateInventarioCommand : IRequest<Response<Guid>>
	{
		public Guid Id { get; set; }
		public OperacionInventario Operacion { get; set; }
		public int Cantidad { get; set; }
	}
	public class UpdateInventarioCommandHandler : IRequestHandler<UpdateInventarioCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<Inventario> _repositoryAsync;

		public UpdateInventarioCommandHandler(IRepositoryAsync<Inventario> repositoryAsync)
		{
			_repositoryAsync = repositoryAsync;
		}

		public async Task<Response<Guid>> Handle(UpdateInventarioCommand request, CancellationToken cancellationToken)
		{
			var inventario = (await _repositoryAsync.ListAsync())
				.Where(x => x.VacunaId == request.Id).FirstOrDefault() ??
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			if (request.Operacion == OperacionInventario.Agregar)
			{
				inventario.CantidadIngresada += request.Cantidad;
				inventario.CantidadDisponible += request.Cantidad;
			}
			else
			{
				if (inventario.CantidadDisponible < request.Cantidad)
				{
					throw new ApiException($"El inventario no puede ser menor que la cantidad a subtraer");
				}

				inventario.CantidadIngresada -= request.Cantidad;
				inventario.CantidadDisponible -= request.Cantidad;
			}

			await _repositoryAsync.UpdateAsync(inventario, cancellationToken);

			return new Response<Guid>(inventario.Id);
		}
	}
}
