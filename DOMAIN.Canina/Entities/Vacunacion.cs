using DOMAIN.Common;
using System;


namespace DOMAIN.Canina.Entities
{
	public class Vacunacion : AuditableBaseEntity
    {

        public Guid CentroId { get; set; }
        public virtual Centro Centro { get; set; }
        public Guid VacunadorId { get; set; }
        public virtual Vacunador Vacunador { get; set; }
        public Guid VacunaId { get; set; }
        public virtual Vacuna Vacuna { get; set; }
        public Guid CaninoId { get; set; }
        public virtual Canino Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }
    }
}
