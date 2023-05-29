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

namespace APLICATION.Feauters.Vacunas.Commands.DeleteVacunaCommand
{
   
    public class DeleteVacunaCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteVacunaCommandHandler : IRequestHandler<DeleteVacunaCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> _repositoryAsync;
        

        public DeleteVacunaCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
          
        }


        public async Task<Response<Guid>> Handle(DeleteVacunaCommand request, CancellationToken cancellationToken)
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
