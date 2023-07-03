using APLICATION.DTOs.User;
using APLICATION.Wrappers;
using System.Threading.Tasks;

namespace APLICATION.Interface
{
	public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAdministradoresAsync(RegisterAdminRequest request, string origin);
		Task<Response<string>> RegisterPropietariosAsync(RegisterPropietarioRequest request, string origin);
		Task<Response<string>> PasswordRecovery(string email);
	}
}
