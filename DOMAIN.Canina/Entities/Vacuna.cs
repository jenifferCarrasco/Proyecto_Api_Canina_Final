using DOMAIN.Common;

namespace DOMAIN.Canina.Entities
{
	public class Vacuna: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        public string Lote { get; set; }
        public Estados Estatus { get; set; }
		public virtual Inventario VacunaInventario { get; set; } = new Inventario();

	}
}
