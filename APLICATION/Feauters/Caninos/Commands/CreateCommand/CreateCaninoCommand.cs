using Application.Interface;
using APLICATION.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Canino.Commands.CreateCommand
{
    public class CreateCaninoCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string PropietarioId { get; set; }
        public Estados Estatus { get; set; }
     

    }
    public class CreateCaninoCommandHandler : IRequestHandler<CreateCaninoCommand, Response<Guid>> {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Caninos> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateCaninoCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<DOMAIN.Canina.Entities.Caninos>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
