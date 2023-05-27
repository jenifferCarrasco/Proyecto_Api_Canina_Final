using DOMAIN.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Canina.Entities
{
    public class Propietarios: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
        public ICollection<Caninos> Caninos { get; set; }
    }
}
