using APLICATION.DTOs.User;
using APLICATION.Enum;
using APLICATION.Exceptions;
using APLICATION.Interface;
using APLICATION.Wrappers;
using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Setting;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PERSISTENCE.Canina.Context;
using PERSISTENCE.Canina.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<Usuario> _userManage;
		private readonly SignInManager<Usuario> _singInManage;
		private readonly JWTSetting _jwtSetting;
		private readonly ApplicationDbContext _context;
		private readonly IEmailService _emailService;

		public AccountService(UserManager<Usuario> userManage,
			IOptions<JWTSetting> jwtSetting = null,
			SignInManager<Usuario> singInManage = null,
			ApplicationDbContext context = null,
			IEmailService emailService = null)
		{
			_userManage = userManage;
			_jwtSetting = jwtSetting.Value;
			_singInManage = singInManage;
			_context = context;
			_emailService = emailService;
		}

		public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
		{
			var usuario = await _context.Users
				.Include(x=>x.Propietario)
				.FirstOrDefaultAsync(x => x.UserName == request.Username)
				?? throw new ApiException($"No existe una cuenta registrada con {request.Username}.");

			var result = await _singInManage.PasswordSignInAsync(usuario.UserName, request.Password,
				false, lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				throw new ApiException($"Usuario o contraseña incorrecta!");
			}

			JwtSecurityToken jwtSecurityToken = await GenerateJWToken(usuario);
			AuthenticationResponse response = new AuthenticationResponse
			{
				Id = usuario.Id,
				PropietarioId = usuario?.Propietario?.Id.ToString() ?? null,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Email = usuario.Email,
				UserName = usuario.UserName
			};

			var rolesLiat = await _userManage.GetRolesAsync(usuario).ConfigureAwait(false);
			response.Roles = rolesLiat.ToList();
			response.IsVerified = usuario.EmailConfirmed;

			var refreshToken = GenerateRefreshToken(ipAddress);
			response.RefreshToken = refreshToken.Token;
			return new Response<AuthenticationResponse>(response);

		}

		public async Task<Response<string>> RegisterAdministradoresAsync(RegisterAdminRequest request, string origin)
		{
			var usuarioExiste = await _userManage.FindByNameAsync(request.UserName);
			var emailExiste = await _userManage.FindByEmailAsync(request.Email);

			if (usuarioExiste != null)
			{
				throw new ApiException($"Este nombre de usuario {request.UserName} ya existe!");
			}

			if (emailExiste != null)
			{
				throw new ApiException($"Este Email de usuario {request.Email} ya existe!");
			}

			var usuario = new Usuario
			{
				TipoUsuario = UserType.Administrador.ToString(),
				Email = request.Email,
				UserName = request.UserName,
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				Administrador = new Administrador
				{
					Nombre = request.Nombre,
					Apellido = request.Apellido,
				}
			};

			var result = await _userManage.CreateAsync(usuario, request.Password);
			if (result.Succeeded)
			{
				await _userManage.AddToRoleAsync(usuario, request.Rol.ToString() ?? Roles.Moderador.ToString());
				return new Response<string>(usuario.Id, message: $"Usuario registrado exitosamente!. {request.UserName}");
			}
			else
			{
				throw new ApiException($"{result.Errors}.");
			}
		}

		public async Task<Response<string>> RegisterPropietariosAsync(RegisterPropietarioRequest request, string origin)
		{
			var usuarioExiste = await _userManage.FindByNameAsync(request.UserName);
			var emailExiste = await _userManage.FindByEmailAsync(request.Email);
			var cedulaExiste = await _context.Propietarios.AllAsync(x => x.Cedula == request.Cedula);

			if (usuarioExiste != null)
			{
				throw new ApiException($"Este nombre de usuario {request.UserName} ya existe!");
			}

			if (emailExiste != null)
			{
				throw new ApiException($"Este Email de usuario {request.Email} ya existe!");
			}
			if (cedulaExiste)
			{
				throw new ApiException($"Esta cedula {request.Cedula} ya existe!");
			}


			var usuario = new Usuario
			{
				TipoUsuario = UserType.Propietario.ToString(),
				Email = request.Email,
				UserName = request.UserName,
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				Propietario = new Propietario
				{
					Nombre = request.Nombre,
					Apellido = request.Apellido,
					Cedula = request.Cedula,
					Telefono = request.Telefono,
					Direccion = request.Direccion,
					Sexo = request.Sexo
				}
			};

			var result = await _userManage.CreateAsync(usuario, request.Password);
			if (result.Succeeded)
			{
				await _userManage.AddToRoleAsync(usuario, Roles.Propietario.ToString());
				return new Response<string>(usuario.Id, message: $"Usuario registrado exitosamente!. {request.UserName}");
			}
			else
			{
				throw new ApiException($"{result.Errors}.");
			}
		}

		public async Task<Response<string>> PasswordRecovery(string email)
		{
			var user = _context.Users.Where(x => x.Email == email).FirstOrDefault() ??
				throw new ApiException("Este email no fue encontrado");

			await _userManage.RemovePasswordAsync(user);

			var newPassword = PasswordHelper.GeneratePassword(10);
			var addNewPassword = await _userManage.AddPasswordAsync(user, newPassword);

			if (!addNewPassword.Succeeded)
			{
				throw new ApiException($"No se pudo reestablecer la contraseña de este usuario");
			}

			var template = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n <title>Recuperación de Contraseña</title>\r\n</head>\r\n<body>\r\n <h1>Recuperación de Contraseña</h1>\r\n <p>Hola {{username}}.</p>\r\n \r\n\r\n <p>Tu nueva contraseña ha sido generada. Utiliza la siguiente contraseña para iniciar sesión y luego cambia tu contraseña en tu perfil.</p>\r\n \r\n <p>Contraseña: <strong>{{password}}</strong></p>\r\n</body>\r\n</html>\r\n";
			
			template = template.Replace("{{username}}", user.UserName);
			template = template.Replace("{{password}}", newPassword);
			string subject = "Recuperación de Contraseña";

			bool sentEmail = await _emailService.SendEmailAsync(email,subject, template);
			if (!sentEmail)
			{
				throw new ApiException("No pudo ser enviado el correo con su nueva contraseña");
			}

			return new Response<string>(user.Id, $"Se ha enviado un correo a {user.Email}");
		}
		private async Task<JwtSecurityToken> GenerateJWToken(Usuario usuario)
		{

			var userClaims = await _userManage.GetClaimsAsync(usuario);
			var roles = await _userManage.GetRolesAsync(usuario);
			var roleClaims = new List<Claim>();

			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim("roles", roles[i]));
			}
			string ipAddress = IpHelper.GetIpAddress();


			if (usuario.Propietario != null)
			{
				userClaims.Add(new Claim("propietarioId", usuario.Propietario.Id.ToString()));
			}

			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
				new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
				new Claim("usuarioId", usuario.Id),
				new Claim("ip",ipAddress),
			}
			.Union(userClaims).Union(roleClaims);


			var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
			var signinCredencials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
					issuer: _jwtSetting.Issuer,
					audience: _jwtSetting.Audience,
					claims: claims,
					expires: DateTime.Now.AddMinutes(double.Parse(_jwtSetting.DurationInMinutes)),
					signingCredentials: signinCredencials
				);
			return jwtSecurityToken;
		}

		private RefreshToken GenerateRefreshToken(string ipAddress)
		{
			return new RefreshToken
			{
				Token = RandomTokenString(),
				Expires = DateTime.Now.AddDays(7),
				Created = DateTime.Now,
				CreatedByIp = ipAddress
			};
		}

		private string RandomTokenString()
		{
			using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
			var randomBytes = new byte[40];
			rngCryptoServiceProvider.GetBytes(randomBytes);
			return BitConverter.ToString(randomBytes).Replace("-", "");
		}
	}
}
