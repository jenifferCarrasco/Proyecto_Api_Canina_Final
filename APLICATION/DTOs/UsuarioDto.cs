using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs
{
    public class UsuarioDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Rol Roles { get; set; }

        public ICollection<Administradores> Administradores { get; set; }
        public Propietarios Propietario { get; set; }
    }
}
