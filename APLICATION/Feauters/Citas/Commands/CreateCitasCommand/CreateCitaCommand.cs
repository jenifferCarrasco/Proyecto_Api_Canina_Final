using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Citas.Commands.CreateCitasCommand
{
    public class CreateCitaCommand : IRequest<Response<Guid>>
    {
        public string CentroId { get; set; }
        public DOMAIN.Canina.Entities.Centro Centro { get; set; }
        public string VacunadorId { get; set; }
        public DOMAIN.Canina.Entities.Vacunador Vacunador { get; set; }
        public string CaninoId { get; set; }
        public DOMAIN.Canina.Entities.Canino Canino { get; set; }
        public DOMAIN.Canina.Estados Estatus { get; set; }
        public DateTime FechaCita { get; set; }
    }
    public class CreateCitaCommandHandler : IRequestHandler<CreateCitaCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCitaCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateCitaCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Canino>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
