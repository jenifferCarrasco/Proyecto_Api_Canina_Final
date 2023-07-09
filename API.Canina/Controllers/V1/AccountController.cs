﻿using APLICATION.DTOs.User;
using APLICATION.Feauters.Authenticate.Command.AuthenticateCommand;
using APLICATION.Feauters.Authenticate.Command.PasswordRecoveryCommand;
using APLICATION.Feauters.Authenticate.Command.RegisterAdminCommand;
using APLICATION.Feauters.Authenticate.Command.RegisterPropietarioCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace API.Canina.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : BaseApiController
	{
		[HttpPost("Login")]
		[SwaggerOperation(Summary = "All Users: Inicio de sesion")]
		public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
		{
			return Ok(await Mediator.Send(new AuthenticateCommand
			{
				Username = request.Username,
				Password = request.Password,
				IpAddress = GenerateIPAddress()
			}));
		}

		[HttpPost("Administrador")]
		[SwaggerOperation(Summary = "Only Administradores: Crear Usuario")]
		public async Task<IActionResult> RegisterAdminAsync(RegisterAdminRequest request)
		{
			return Ok(await Mediator.Send(new RegisterAdminCommand
			{
				Nombre = request.Nombre,
				Apellido = request.Apellido,
				Email = request.Email,
				Rol = request.Rol,
				Password = request.Password,
				UserName = request.UserName,
				Origin = Request.Headers["origin"]
			}));
		}

		[HttpPost("Propietario")]
		[SwaggerOperation(Summary = "Only Propietario: Crear Usuario")]
		public async Task<IActionResult> RegisterPropietarioAsync(RegisterPropietarioRequest request)
		{
			return Ok(await Mediator.Send(new RegisterPropietarioCommand
			{
				Nombre = request.Nombre,
				Apellido = request.Apellido,
				Cedula = request.Cedula,
				Sexo = request.Sexo,
				Telefono = request.Telefono,
				Direccion = request.Direccion,
				Email = request.Email,
				Password = request.Password,
				UserName = request.UserName,
				Origin = Request.Headers["origin"]
			}));
		}

		[HttpPost("RecuperarContrasena")]
		[SwaggerOperation(Summary = "All Users: Recuperar contrasena")]
		public async Task<IActionResult> RegisterPropietarioAsync(PasswordRecoveryCommand command)
		{
			return Ok(await Mediator.Send(command));
		}

		private string GenerateIPAddress()
		{

			if (Request.Headers.ContainsKey("X-Forwarded-For"))
				return Request.Headers["X-Forwarded-For"];
			else
				return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}
	}
}
