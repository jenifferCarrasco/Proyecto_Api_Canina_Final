using DOMAIN.Common;
using System;
using System.Collections.Generic;

namespace DOMAIN.Canina.Entities
{
    public class Caninos : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string PropietarioId { get; set; }
        public Propietarios Propietario { get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Vacunaciones> Vacunaciones { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
