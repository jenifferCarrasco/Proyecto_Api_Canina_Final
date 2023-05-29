using APLICATION.Interface;
using APLICATION.Wrappers;
using APLICATION.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using APLICATION.DTOs.User;

namespace APLICATION.Feauters.Authenticate.Command.RegisterCommand
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Roles? Rol { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountService _accountService;

        public RegisterCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAdministradoresAsync(new RegisterRequest
            {

                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Rol = request.Rol,

            }, request.Origin);
        }
    }
}
