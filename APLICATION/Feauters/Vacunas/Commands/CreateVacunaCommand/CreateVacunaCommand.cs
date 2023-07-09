using APLICATION.Exceptions;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand
{
	public class CreateVacunaCommand : IRequest<Response<Guid>>
	{
		public string Nombre { get; set; }
		public string Laboratorio { get; set; }
		public string Descripcion { get; set; }
		public string Lote { get; set; }
		public int Cantidad { get; set; }
	}
	public class CreateVacunaCommandHandler : IRequestHandler<CreateVacunaCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<Vacuna> _repositoryAsync;
		private readonly IMapper _mapper;

		public CreateVacunaCommandHandler(IRepositoryAsync<Vacuna> repositoryAsync, IMapper mapper = null)
		{
			_repositoryAsync = repositoryAsync;
			_mapper = mapper;
		}


		public async Task<Response<Guid>> Handle(CreateVacunaCommand request, CancellationToken cancellationToken)
		{
			var newRegister = _mapper.Map<Vacuna>(request);

			if (request.Cantidad > 0)
			{
				newRegister.VacunaInventario.CantidadIngresada = request.Cantidad;
				newRegister.VacunaInventario.CantidadDisponible = request.Cantidad;
				newRegister.Estatus = DOMAIN.Canina.Estados.Activo;
			}
			else
			{
				newRegister.Estatus = DOMAIN.Canina.Estados.Inactivo;
			}

			var loteIgual = (await _repositoryAsync.ListAsync())
				.Where(x => x.Lote == request.Lote.ToUpper() && x.Nombre == request.Nombre)
				.FirstOrDefault();

			if (loteIgual != null)
				throw new ApiException($"Ya ha sido registrado este lote {request.Lote} para esta vacuna");

			var data = await _repositoryAsync.AddAsync(newRegister);

			return new Response<Guid>(data.Id);
		}
	}
}
