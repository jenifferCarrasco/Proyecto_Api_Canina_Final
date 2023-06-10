using APLICATION.Wrappers;
using Application.Interface;
using AutoMapper;
using DOMAIN.Canina.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Centros.Commands.CreateCentroCommand
{

	public class CreateCentroCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }

    }
    public class CreateCentroCommandHandler : IRequestHandler<CreateCentroCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<Centro> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCentroCommandHandler(IRepositoryAsync<Centro> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateCentroCommand request, CancellationToken cancellationToken)
        {
            var newRegister = _mapper.Map<Centro>(request);
            var data = await _repositoryAsync.AddAsync(newRegister);

            return new Response<Guid>(data.Id);
        }
    }
}
