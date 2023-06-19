using DOMAIN.Canina.Enum;
using DOMAIN.Common;
using System.Collections.Generic;

namespace DOMAIN.Canina.Entities
{
	public class Propietario: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public Generos Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Canino> Caninos { get; set; }
		public virtual ICollection<Cita> Citas { get; set; }
	}
}
