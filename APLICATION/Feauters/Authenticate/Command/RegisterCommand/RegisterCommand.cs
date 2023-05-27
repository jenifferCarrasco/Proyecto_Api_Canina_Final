using APLICATION.Interface;
using APLICATION.Wrappers;
using APLICATION.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Authenticate.Command.RegisterCommand
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        //public string Cedula { get; set; }
        //public string Direccion { get; set; }
        public Roles Roles { get; set; }
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
            return await _accountService.RegisterAsync(new DTOs.User.RegisterRequest
            {

                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                //Cedula = request.Cedula,
                //Direccion = request.Direccion,
                Roles = request.Roles,
               

            }, request.Origin);
        }
    }
}
