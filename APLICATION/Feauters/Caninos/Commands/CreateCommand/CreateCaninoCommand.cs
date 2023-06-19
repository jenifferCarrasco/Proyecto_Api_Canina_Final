using APLICATION.Exceptions;
using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Enum;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Canino.Commands.CreateCommand
{
	public class CreateCaninoCommand : IRequest<Response<Guid>>
	{
		public string Nombre { get; set; }
		public string Raza { get; set; }
		public Generos Sexo { get; set; }
		public string Peso { get; set; }
		public string Color { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public Guid PropietarioId { get; set; }


	}
	public class CreateCaninoCommandHandler : IRequestHandler<CreateCaninoCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
		private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> _repositoryPropietarioAsync;
		private readonly IMapper _mapper;

		public CreateCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper = null, IRepositoryAsync<DOMAIN.Canina.Entities.Propietario> repositoryPropietarioAsync = null)
		{
			_repositoryAsync = repositoryAsync;
			_mapper = mapper;
			_repositoryPropietarioAsync = repositoryPropietarioAsync;
		}


		public async Task<Response<Guid>> Handle(CreateCaninoCommand request, CancellationToken cancellationToken)
		{
			var propietarioExiste = await _repositoryPropietarioAsync.GetByIdAsync(request.PropietarioId);

			if (propietarioExiste is null)
			{
				throw new ApiException("Este Propietario no existe");
			}

			var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Canino>(request);
			var data = await _repositoryAsync.AddAsync(newRegister);

			return new Response<Guid>(data.Id);
		}
	}
}
