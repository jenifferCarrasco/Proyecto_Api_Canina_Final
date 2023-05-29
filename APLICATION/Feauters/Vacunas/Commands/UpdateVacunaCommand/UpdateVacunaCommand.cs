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

namespace APLICATION.Feauters.Vacunas.Commands.UpdateVacunaCommand
{
    
    public class UpdateVacunaCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        //public DateTime FechaCaducidad { get; set; }
        //public string Lote { get; set; }
        //public DOMAIN.Canina.Estados Estatus { get; set; }
        

    }
    public class UpdateVacunaCommandHandler : IRequestHandler<UpdateVacunaCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateVacunaCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Vacuna> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdateVacunaCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else {
                client.Nombre = request.Nombre;
                client.Laboratorio = request.Laboratorio;
                client.Descripcion = request.Descripcion;
                //client.Lote = request.Lote;
                //client.FechaCaducidad = request.FechaCaducidad;
                //client.Estatus = request.Estatus;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
