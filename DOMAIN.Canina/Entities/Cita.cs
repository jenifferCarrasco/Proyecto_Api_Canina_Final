using DOMAIN.Common;
using System;

namespace DOMAIN.Canina.Entities
{
	public class Cita : AuditableBaseEntity
    {
        public Guid CentroId { get; set; }
        public virtual  Centro Centro { get; set; }
        public Guid VacunadorId { get; set; }
        public virtual Vacunador Vacunador { get; set; }
        public Guid CaninoId { get; set; }
        public virtual Canino Canino { get; set; }
        public Estados Estatus { get; set; }
        public DateTime FechaCita { get; set; }

    }
}
