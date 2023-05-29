using APLICATION.DTOs.User;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace APLICATION.DTOs
{
    public class VacunacionesDto
    {
        public string Id { get; set; }
        public string CentroId { get; set; }
        public Centro Centro { get; set; }
        public string VacunadorId { get; set; }
        public Vacunador Vacunador { get; set; }
        public string VacunaId { get; set; }
        public Vacuna Vacuna { get; set; }
        public string CaninoId { get; set; }
        public Canino Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }
    }
}
