using APLICATION.Wrappers;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Centros.Commands.DeleteCentroCommand
{
	public class DeleteCentroCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCentroCommandHandler : IRequestHandler<DeleteCentroCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centro> _repositoryAsync;
        public DeleteCentroCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centro> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }

        public async Task<Response<Guid>> Handle(DeleteCentroCommand request, CancellationToken cancellationToken)
        {
            var centro = await _repositoryAsync.GetByIdAsync(request.Id);
            if (centro == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                await _repositoryAsync.DeleteAsync(centro);

                return new Response<Guid>(centro.Id);
            }
        }
    }
}
