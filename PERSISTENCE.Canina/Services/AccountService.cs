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
        private readonly UserManager<Usuarios> _userManage;
        private readonly RoleManager<Usuarios> _roleManage;
        private readonly SignInManager<Usuarios> _singInManage;
        private readonly JWTSetting _jwtSetting;

        public AccountService(UserManager<Usuarios> userManage, RoleManager<Usuarios> roleManage = null, IOptions<JWTSetting> jwtSetting = null ,SignInManager<Usuarios> singInManage = null)
        {
            _userManage = userManage;
            _roleManage = roleManage;
            _jwtSetting = jwtSetting.Value;
            _singInManage = singInManage;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var usuario = await _userManage.FindByEmailAsync(request.Email)
                ?? throw new ApiException($"No existe una cuenta registrada con el Email {request.Email}.");

			var result = await _singInManage.PasswordSignInAsync(usuario.UserName, request.Password,
                false, lockoutOnFailure:false);
            if (!result.Succeeded) {
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

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var usuarioSame = await _userManage.FindByNameAsync(request.UserName);
            if (usuarioSame != null) {
                throw new ApiException($"Este nombre de usuario {request.UserName} ya existe!");
            }
            var usuario = new Usuarios
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.UserName,
                //Cedula = request.Cedula,
                //Direccion = request.Direccion,
                //Roles = request.Roles,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };


            var usuarioSameEmail = await _userManage.FindByEmailAsync(request.Email);
            if (usuarioSameEmail != null)
            {

                throw new ApiException($"Este Email de usuario {request.Email} ya existe!");
            }
            else {
                var result = await _userManage.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    //await _userManage.AddToRoleAsync(usuario, Roles.Moderador.ToString());
                    //await _userManage.AddToRoleAsync(usuario, Roles.Vacunador.ToString());

                    Roles rol;
                    if (((int)usuario.Roles) == 0)
                    {
                        rol = Roles.Admin;
                    }
                    else if(((int)usuario.Roles) == 1)
                    {

                        rol = Roles.Moderador;
                    }
                    else if (((int)usuario.Roles) == 2)
                    {
                        rol = Roles.Paciente;

                    }
                    else
                    {
                        rol = Roles.Vacunador;

                    }
                    await _userManage.AddToRoleAsync(usuario, rol.ToString());
                    return new Response<string>(usuario.Id, message: $"Usuario registrado exitosamente!. {request.UserName}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}.");
                }
            }



        }

        private async Task<JwtSecurityToken> GenerateJWToken(Usuarios usuario) {

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
                    expires: DateTime.Now.AddMinutes(double.Parse( _jwtSetting.DurationInMinutes) ),
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
