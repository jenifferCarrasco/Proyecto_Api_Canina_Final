using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Seeds
{
	public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager) 
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Propietario.ToString()));
        }
    }
}
