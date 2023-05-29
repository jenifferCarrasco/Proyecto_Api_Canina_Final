using DOMAIN.Common;
using System;

namespace DOMAIN.Canina.Entities
{
	public class Vacuna: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Lote { get; set; }
        public Estados Estatus { get; set; }

    }
}
