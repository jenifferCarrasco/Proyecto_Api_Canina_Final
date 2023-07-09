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
using DOMAIN.Canina.Enum;

namespace APLICATION.Feauters.Vacunadores.Commands.UpdateCommand
{
    public class UpdateVacunadoresCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
		public Generos Sexo { get; set; }
		public string Direccion { get; set; }
        public string Telefono { get; set; }

    }
    public class UpdateVacunadoresCommandHandler : IRequestHandler<UpdateVacunadoresCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacunador> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateVacunadoresCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacunador> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdateVacunadoresCommand request, CancellationToken cancellationToken)
        {
            var vacunador = await _repositoryAsync.GetByIdAsync(request.Id);
            if (vacunador == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                vacunador.Nombre = request.Nombre;
                vacunador.Apellido = request.Apellido;
                vacunador.Cedula = request.Cedula;
                vacunador.Telefono = request.Telefono;
                vacunador.Direccion = request.Direccion;
                vacunador.Sexo = request.Sexo;


                await _repositoryAsync.UpdateAsync(vacunador);

                return new Response<Guid>(vacunador.Id);
            }
        }
    }
}
