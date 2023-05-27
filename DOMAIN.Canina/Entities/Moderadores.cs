using DOMAIN.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Canina.Entities
{
    public class Moderadores : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
