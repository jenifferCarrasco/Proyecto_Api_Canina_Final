using DOMAIN.Common;
using System;

namespace DOMAIN.Canina.Entities
{
	public class Inventario : AuditableBaseEntity
	{
		public Guid VacunaId { get; set; }
		public int CantidadIngresada { get; set; }
		public int CantidadUtilizada { get; set; }
		public int CantidadDisponible { get; set; }
		public virtual Vacuna Vacuna { get; set; }
	}
}
