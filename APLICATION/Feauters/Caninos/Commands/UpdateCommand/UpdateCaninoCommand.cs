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

namespace APLICATION.Feauters.Caninos.Commands.UpdateCommand
{
    
    public class UpdateCaninoCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string PropietarioId { get; set; }
        public Estados Estatus { get; set; }

    }
    public class UpdateCaninoCommandHandler : IRequestHandler<UpdateCaninoCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdateCaninoCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else {
                client.Nombre = request.Nombre;
                client.Raza = request.Raza;
                client.FechaNacimiento = request.FechaNacimiento;
                client.Sexo = request.Sexo;
                client.Peso = request.Peso;
                client.PropietarioId = request.PropietarioId;
                client.Color = request.Color;
                client.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
