using APLICATION.DTOs.User;
using APLICATION.Feauters.Authenticate.Command.AuthenticateCommand;
using APLICATION.Feauters.Authenticate.Command.RegisterCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Canina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request) {
            return Ok(await Mediator.Send(new AuthenticateCommand { 
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIPAddress()
            }));
        }
        [HttpPost("admin")]
        public async Task<IActionResult> AdminRegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Rol = request.Rol,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Origin = Request.Headers["origin"]
                
            }));
        }

        private string GenerateIPAddress() {

            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
