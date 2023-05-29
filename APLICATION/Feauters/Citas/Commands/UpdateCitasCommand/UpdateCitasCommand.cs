using APLICATION.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using APLICATION.DTOs;
using DOMAIN.Canina;
using Application.Interface;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace APLICATION.Feauters.Citas.Commands.UpdateCitasCommand
{
    public class UpdateCitasCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CentroId { get; set; }
        public CentrosDto Centro { get; set; }
        public Guid VacunadorId { get; set; }
        public VacunadoresDto Vacunadores { get; set; }
        public Guid CaninoId { get; set; }
        public CaninoDto Canino { get; set; }
        public Estados Estatus { get; set; }
        public DateTime FechaCita { get; set; }
    }
    public class UpdateCitasCommandHandler : IRequestHandler<UpdateCitasCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Cita> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCitasCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Cita> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(UpdateCitasCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                client.FechaCita = request.FechaCita;
                client.VacunadorId = request.VacunadorId;
                client.CaninoId = request.CaninoId;
                client.CentroId = request.CentroId;

                await _repositoryAsync.UpdateAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
