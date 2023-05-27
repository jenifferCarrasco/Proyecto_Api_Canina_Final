using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Citas.Commands.DeleteCitasCommand
{
    public class DeleteCitasCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCitasCommandHandler : IRequestHandler<DeleteCitasCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Citas> _repositoryAsync;


        public DeleteCitasCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Citas> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;

        }


        public async Task<Response<Guid>> Handle(DeleteCitasCommand request, CancellationToken cancellationToken)
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
