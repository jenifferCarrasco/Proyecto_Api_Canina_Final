using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using DOMAIN.Common;
using Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Proyect.Entities
{
    public class ICaninos : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        //propietario perro
        public ApplicationUser applicationUser { get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Vacunaciones> Vacunaciones { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
