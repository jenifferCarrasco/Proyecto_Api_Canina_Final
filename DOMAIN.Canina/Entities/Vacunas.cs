using DOMAIN.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOMAIN.Canina.Entities
{
    public class Vacunas: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Lote { get; set; }
        public Estados Estatus { get; set; }

    }
}
