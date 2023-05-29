using APLICATION.DTOs.User;
using APLICATION.Enum;
using APLICATION.Exceptions;
using APLICATION.Interface;
using APLICATION.Wrappers;
using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Setting;
using Microsoft.AspNetCore.Identity;
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
		private readonly RoleManager<Usuario> _roleManage;
		private readonly SignInManager<Usuario> _singInManage;
		private readonly JWTSetting _jwtSetting;
		private readonly ApplicationDbContext _context;

		public AccountService(UserManager<Usuario> userManage,
			RoleManager<Usuario> roleManage = null,
			IOptions<JWTSetting> jwtSetting = null,
			SignInManager<Usuario> singInManage = null,
			ApplicationDbContext context = null)
		{
			_userManage = userManage;
			_roleManage = roleManage;
			_jwtSetting = jwtSetting.Value;
			_singInManage = singInManage;
			_context = context;
		}

		public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
		{
			var usuario = await _userManage.FindByEmailAsync(request.Email)
				?? throw new ApiException($"No existe una cuenta registrada con el Email {request.Email}.");

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
				JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Email = usuario.Email,
				UserName = usuario.UserName
			};

			var rolesLiat = await _userManage.GetRolesAsync(usuario).ConfigureAwait(false);
			response.Roles = rolesLiat.ToList();
			response.IsVerified = usuario.EmailConfirmed;

			var refreshToken = GenerateRefreshToken(ipAddress);
			response.RefreshToken = refreshToken.Token;
			return new Response<AuthenticationResponse>(response, $"Usuario Autenticado {usuario.UserName}");

		}

		public async Task<Response<string>> RegisterAdministradoresAsync(RegisterRequest request, string origin)
		{
			var usuarioSame = await _userManage.FindByNameAsync(request.UserName);
			var usuarioSameEmail = await _userManage.FindByEmailAsync(request.Email);

			if (usuarioSame != null)
			{
				throw new ApiException($"Este nombre de usuario {request.UserName} ya existe!");
			}

			if (usuarioSameEmail != null)
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

			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
				new Claim("uid", usuario.Id),
				new Claim("ip",ipAddress)
			}
			.Union(userClaims)
			.Union(roleClaims);

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
