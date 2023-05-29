using APLICATION.Exceptions;
using Application.Interface;
using APLICATION.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.DeleteVacunacionCommand
{
   
    public class DeleteVacunacionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteVacunacionCommandHandler : IRequestHandler<DeleteVacunacionCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunacion> _repositoryAsync;
        

        public DeleteVacunacionCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunacion> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
          
        }


        public async Task<Response<Guid>> Handle(DeleteVacunacionCommand request, CancellationToken cancellationToken)
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
