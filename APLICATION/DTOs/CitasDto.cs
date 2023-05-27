using APLICATION.DTOs.User;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace APLICATION.DTOs
{
    public class CitasDto
    {
        public string Id { get; set; }
        public string CentroId { get; set; }
        public Centros Centro { get; set; }
        public string VacunadorId { get; set; }
        public Vacunadores Vacunador { get; set; }
        public string CaninoId { get; set; }
        public Caninos Canino { get; set; }
        public Estados Estatus { get; set; }
        public DateTime FechaCita { get; set; }
    }
}
