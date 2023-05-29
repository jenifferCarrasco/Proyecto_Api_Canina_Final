using APLICATION.DTOs.User;
using APLICATION.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APLICATION.Interface
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAdministradoresAsync(RegisterRequest request, string origin);
    }
}
