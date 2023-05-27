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
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Propietarios.Commands.UpdateCommand
{

    public class UpdatePropietarioCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string UsuarioId { get; set; }

    }
    public class UpdatePropietarioCommandHandler : IRequestHandler<UpdatePropietarioCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Propietarios> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdatePropietarioCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Propietarios> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdatePropietarioCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                client.Nombre = request.Nombre;
                client.Apellido = request.Apellido;
                client.Cedula = request.Cedula;
                client.Telefono = request.Telefono;
                client.Direccion = request.Direccion;
                

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
