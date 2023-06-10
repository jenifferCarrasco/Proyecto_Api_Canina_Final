using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Enum;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Seeds
{
	public static class DefaultPropietarioUser
    {
        public static async Task SeedAsync(UserManager<Usuario> userManager)
        {
            var defaultUser = new Usuario
            {
                Propietario = new Propietario {

                    Nombre = "Jeniffer",
                    Apellido = "Carrasco",
                    Cedula = "40211098734",
                    Direccion = "Santo Domingo Este",
                    Telefono = "8097654562",
                    Sexo = Generos.Femenino
                },
                TipoUsuario = UserType.Propietario.ToString(),
                UserName = "userPropietario",
                Email = "propietario@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {

                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "S3v3r1na");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Propietario.ToString());

                }
            }
        }

    }
}
