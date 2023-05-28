﻿using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PERSISTENCES.Canina.Seeds
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