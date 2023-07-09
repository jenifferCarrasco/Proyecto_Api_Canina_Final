using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Admin.Commands.DeleteAdmin
{
	public class DeleteAdminCommand : IRequest<Response<Guid>>
	{
		public Guid Id { get; set; }
	}
	public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Response<Guid>>
	{
		private readonly IRepositoryAsync<Usuario> _usuarioRepository;
		private readonly IRepositoryAsync<Administrador> _administradorRepository;

		public DeleteAdminCommandHandler(IRepositoryAsync<Administrador> administradorRepository, IRepositoryAsync<Usuario> usuarioRepository)
		{
			_administradorRepository = administradorRepository;
			_usuarioRepository = usuarioRepository;
		}

		public async Task<Response<Guid>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
		{
			var admin = await _administradorRepository.GetByIdAsync(request.Id, cancellationToken) ??
				throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");

			var user = await _usuarioRepository.GetByIdAsync(admin.UsuarioId, cancellationToken);

			await _administradorRepository.DeleteAsync(admin, cancellationToken);
			await _usuarioRepository.DeleteAsync(user, cancellationToken);

			return new Response<Guid>(admin.Id);
		}
	}
}
