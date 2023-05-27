using DOMAIN.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Canina.Entities
{
    public class Centros : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion{ get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
        public virtual ICollection<Vacunaciones> Vacunaciones { get; set; }
    }
}
