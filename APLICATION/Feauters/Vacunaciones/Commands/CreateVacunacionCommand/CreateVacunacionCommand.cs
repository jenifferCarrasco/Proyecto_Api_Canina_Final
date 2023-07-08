using APLICATION.Exceptions;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand
{
	public class CreateVacunacionCommand : IRequest<Response<Guid>>
	{
		public Guid CentroId { get; set; }
		public Guid VacunadorId { get; set; }
		public Guid VacunaId { get; set; }
		public Guid CaninoId { get; set; }
		public int Dosis { get; set; }
		public DateTime FechaProxima { get; set; }

	}
	public class CreateVacunacionCommandHandler : IRequestHandler<CreateVacunacionCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<Vacunacion> _vacunacionRepositoryAsync;
		private readonly IRepositoryAsync<Inventario> _inventarioRepositoryAsync;
		private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _caninoRepositoryAsync;
		private readonly IRepositoryAsync<Vacunador> _vacunadorRepositoryAsync;
		private readonly IRepositoryAsync<Centro> _centroRepositoryAsync;

		private readonly IMapper _mapper;

		public CreateVacunacionCommandHandler(IMapper mapper = null,
			IRepositoryAsync<Inventario> inventarioRepositoryAsync = null,
			IRepositoryAsync<DOMAIN.Canina.Entities.Canino> caninoRepositoryAsync = null,
			IRepositoryAsync<Vacunacion> vacunacionRepositoryAsync = null,
			IRepositoryAsync<Vacunador> vacunadorRepositoryAsync = null,
			IRepositoryAsync<Centro> centroRepositoryAsync = null)
		{
			_mapper = mapper;
			_inventarioRepositoryAsync = inventarioRepositoryAsync;
			_caninoRepositoryAsync = caninoRepositoryAsync;
			_vacunacionRepositoryAsync = vacunacionRepositoryAsync;
			_vacunadorRepositoryAsync = vacunadorRepositoryAsync;
			_centroRepositoryAsync = centroRepositoryAsync;
		}


		public async Task<Response<Guid>> Handle(CreateVacunacionCommand request, CancellationToken cancellationToken)
		{
			var nuevaVacunacion = _mapper.Map<Vacunacion>(request);

			var inventario = (await _inventarioRepositoryAsync.ListAsync())
				.Where(x => x.VacunaId == request.VacunaId).FirstOrDefault() ??
				throw new KeyNotFoundException($"Vacuna no encontrada con el id {request.VacunaId}");

			if (inventario.CantidadDisponible < 0) throw new ApiException($"Esta vacuna no esta disponible");

			inventario.CantidadDisponible--;
			inventario.CantidadUtilizada++;

			var vacunador = (await _vacunadorRepositoryAsync.ListAsync())
				.Where(x => x.Id == request.VacunadorId).FirstOrDefault() ??
				throw new KeyNotFoundException($"Vacunador no encontrado con el id {request.VacunaId}");

			var canino = (await _caninoRepositoryAsync.ListAsync())
				.Where(x => x.Id == request.CaninoId).FirstOrDefault() ??
				throw new KeyNotFoundException($"Canino no encontrado con el id {request.VacunaId}");

			var centro = (await _centroRepositoryAsync.ListAsync())
				.Where(x => x.Id == request.CentroId).FirstOrDefault() ??
				throw new KeyNotFoundException($"Centro no encontrado con el id {request.VacunaId}");

			var vacunacion = (await _vacunacionRepositoryAsync
				.ListAsync())
				.Where(x => x.VacunaId == request.VacunaId &&
					   x.Dosis == nuevaVacunacion.Dosis &&
					   x.CaninoId == request.CaninoId)
				.FirstOrDefault();

			if (vacunacion != null) throw new ApiException($"Ya este canino tiene esta vacuna");

			var data = await _vacunacionRepositoryAsync.AddAsync(nuevaVacunacion);
			await _inventarioRepositoryAsync.UpdateAsync(inventario);

			return new Response<Guid>(data.Id);
		}

	}
}
