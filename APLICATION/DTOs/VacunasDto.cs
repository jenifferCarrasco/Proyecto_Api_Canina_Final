using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs
{
    public class VacunasDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Lote { get; set; }
        public DOMAIN.Canina.Estados Estatus { get; set; }
    }
}
