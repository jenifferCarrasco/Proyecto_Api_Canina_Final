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
        public UserType Roles { get; set; }

        public ICollection<Administrador> Administradores { get; set; }
        public Propietario Propietario { get; set; }
    }
}
