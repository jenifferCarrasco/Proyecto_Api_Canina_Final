using APLICATION.Interface;
using APLICATION.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Authenticate.Command.PasswordRecoveryCommand
{
	public class PasswordRecoveryCommand : IRequest<Response<string>>
	{
		public string Email { get; set; }
	}

	public class PasswordRecoveryCommandHandler : IRequestHandler<PasswordRecoveryCommand, Response<string>>
	{
		private readonly IAccountService _accountService;
		public PasswordRecoveryCommandHandler(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<Response<string>> Handle(PasswordRecoveryCommand request, CancellationToken cancellationToken)
		{
			return await _accountService.PasswordRecovery(request.Email);
		}
	}
}
