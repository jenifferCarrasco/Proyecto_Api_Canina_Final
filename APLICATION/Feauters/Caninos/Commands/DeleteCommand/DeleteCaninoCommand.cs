using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Clientes.Commands.DeleteClientCommand
{

	public class DeleteCaninoCommand : IRequest<Response<Guid>>
	{
		public Guid Id { get; set; }
	}
	public class DeleteCaninoCommandHandler : IRequestHandler<DeleteCaninoCommand, Response<Guid>>
	{

		private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
		public DeleteCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync)
		{
			_repositoryAsync = repositoryAsync;

		}

		public async Task<Response<Guid>> Handle(DeleteCaninoCommand request, CancellationToken cancellationToken)
		{
			var canino = await _repositoryAsync.GetByIdAsync(request.Id) ??
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			await _repositoryAsync.DeleteAsync(canino);

			return new Response<Guid>(canino.Id);
		}
	}
}
