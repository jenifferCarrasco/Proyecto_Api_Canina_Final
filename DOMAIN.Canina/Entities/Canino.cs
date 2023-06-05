using DOMAIN.Canina.Enum;
using DOMAIN.Common;
using System;
using System.Collections.Generic;

namespace DOMAIN.Canina.Entities
{
	public class Canino : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public Generos Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid PropietarioId { get; set; }
        public virtual Propietario Propietario { get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Vacunacion> Vacunaciones { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
    }
}
