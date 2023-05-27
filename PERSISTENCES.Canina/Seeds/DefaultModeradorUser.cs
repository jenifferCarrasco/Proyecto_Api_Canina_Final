using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERSISTENCES.Canina.Seeds
{
    public static class DefaultModeradorUser
    {
        public static async Task SeedAsync(UserManager<Usuarios> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new Usuarios
            {
                Moderadores = new List<Moderadores> {

                    new Moderadores {
                        Nombre = "Jeniffer",
                        Apellido = "Carrasco",

                    }

                },
                Nombre = "Jeniffer",
                Apellido = "Carrasco",
                UserName = "userAdmin1",
                Email = "userAdmin@gmail.com1",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {

                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "S3v3r1na");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderador.ToString());

                }
            }
        }
    }
}
