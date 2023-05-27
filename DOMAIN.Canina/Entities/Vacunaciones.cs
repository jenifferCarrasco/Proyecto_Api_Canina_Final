using DOMAIN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace DOMAIN.Canina.Entities
{
    public class Vacunaciones : AuditableBaseEntity
    {

        public string CentroId { get; set; }
        public Centros Centro { get; set; }
        public string VacunadorId { get; set; }
        public Vacunadores Vacunador { get; set; }
        public string VacunaId { get; set; }
        public Vacunas Vacuna { get; set; }
        public string CaninoId { get; set; }
        public Caninos Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }
    }
}
