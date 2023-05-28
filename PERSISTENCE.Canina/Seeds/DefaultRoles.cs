using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Seeds
{
	public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<Usuarios> userManager, RoleManager<IdentityRole> roleManager) 
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Vacunador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Paciente.ToString()));
        }
    }
}
