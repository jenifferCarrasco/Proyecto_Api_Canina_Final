using APLICATION.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Canina.Entities
{
    public class Usuarios:IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Rol Roles { get; set; }

        public ICollection<Administradores> Administradores { get; set; }
        public ICollection<Moderadores> Moderadores { get; set; }
        public Propietarios Propietario { get; set; }
        public Vacunadores Vacunadores{ get; set; }

    }
    
}
