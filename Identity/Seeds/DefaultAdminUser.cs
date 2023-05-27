using APLICATION.Enum;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "userAdmin",
                Email = "userAdmin@gmail.com",
                Nombre = "Jeniffer",
                Apellido = "Carrasco",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id)) {

                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null) {
                    await userManager.CreateAsync(defaultUser, "S3v3r1na");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Vacunador.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderador.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Paciente.ToString());

                }
            }
        }
    }
}
