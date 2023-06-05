using APLICATION.DTOs.User;
using APLICATION.Interface;
using APLICATION.Wrappers;
using DOMAIN.Canina.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Authenticate.Command.RegisterPropietarioCommand
{
	public class RegisterPropietarioCommand : IRequest<Response<string>>
    {
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Cedula { get; set; }
        public Generos Sexo { get; set; }
        public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Origin { get; set; }
    }

    public class RegisterPropietarioCommandHandler : IRequestHandler<RegisterPropietarioCommand, Response<string>>
    {
        private readonly IAccountService _accountService;

        public RegisterPropietarioCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Response<string>> Handle(RegisterPropietarioCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterPropietariosAsync(new RegisterPropietarioRequest
            {
                Cedula = request.Cedula,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Sexo = request.Sexo,
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
            }, request.Origin);
        }
    }
}
