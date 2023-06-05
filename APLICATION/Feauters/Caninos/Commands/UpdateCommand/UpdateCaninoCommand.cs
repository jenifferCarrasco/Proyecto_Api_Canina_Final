using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina;
using DOMAIN.Canina.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Caninos.Commands.UpdateCommand
{

	public class UpdateCaninoCommand : IRequest<Response<string>>
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
		public string Raza { get; set; }
		public Generos Sexo { get; set; }
		public string Peso { get; set; }
		public string Color { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public Estados Estatus { get; set; }

	}
	public class UpdateCaninoCommandHandler : IRequestHandler<UpdateCaninoCommand, Response<string>>
	{

		private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;

		public UpdateCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync)
		{
			_repositoryAsync = repositoryAsync;
		}


		public async Task<Response<string>> Handle(UpdateCaninoCommand request, CancellationToken cancellationToken)
		{
			var canino = await _repositoryAsync.GetByIdAsync(request.Id) ??
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			canino.Nombre = request.Nombre;
			canino.Raza = request.Raza;
			canino.FechaNacimiento = request.FechaNacimiento;
			canino.Sexo = request.Sexo;
			canino.Peso = request.Peso;
			canino.Color = request.Color;
			canino.Estatus = request.Estatus;

			await _repositoryAsync.UpdateAsync(canino);

			return new Response<string>(canino.Id.ToString());
		}
	}
}

