using DOMAIN.Common;
using System.Collections.Generic;

namespace DOMAIN.Canina.Entities
{
	public class Centro : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion{ get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<Vacunacion> Vacunaciones { get; set; }
    }
}
