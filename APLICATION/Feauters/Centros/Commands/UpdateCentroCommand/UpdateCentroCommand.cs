using APLICATION.Wrappers;
using Application.Interface;
using DOMAIN.Canina;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Centros.Commands.UpdateCentroCommand
{
	public class UpdateCentroCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Estados Estatus { get; set; } = Estados.Activo;
    }
    public class UpdateCentroCommandHandler : IRequestHandler<UpdateCentroCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centro> _repositoryAsync;

        public UpdateCentroCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centro> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }


        public async Task<Response<Guid>> Handle(UpdateCentroCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                client.Nombre = request.Nombre;
                client.Direccion = request.Direccion;
                client.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
