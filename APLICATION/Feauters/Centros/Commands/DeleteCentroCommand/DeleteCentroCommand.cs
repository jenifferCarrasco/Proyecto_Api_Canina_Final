using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Centros> _repositoryAsync;


        public DeleteCentroCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Centros> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;

        }


        public async Task<Response<Guid>> Handle(DeleteCentroCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {

                await _repositoryAsync.DeleteAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
