using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Seeds
{
	public static class DefaultAdminUser
	{
		public static async Task SeedAsync(UserManager<Usuario> userManager)
		{
			var defaultUser = new Usuario
			{
				Administrador = new Administrador
				{
					Nombre = "Jeniffer",
					Apellido = "Carrasco",
				},
				UserName = "userAdmin",
				Email = "userAdmin@gmail.com",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				TipoUsuario = UserType.Administrador.ToString(),
			};

			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{

				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "S3v3r1na");
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
				}
			}
		}
	}
}
