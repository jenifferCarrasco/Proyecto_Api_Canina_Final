using APLICATION.DTOs.User;
using APLICATION.Enum;
using APLICATION.Interface;
using APLICATION.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Authenticate.Command.RegisterAdminCommand
{
	public class RegisterAdminCommand : IRequest<Response<string>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Roles? Rol { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, Response<string>>
    {
        private readonly IAccountService _accountService;

        public RegisterAdminCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Response<string>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAdministradoresAsync(new RegisterAdminRequest
            {

                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Rol = request.Rol,
            }, request.Origin);
        }
    }
}
