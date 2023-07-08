using DOMAIN.Canina.Entities;
using DOMAIN.Canina.Enum;
using System;

namespace APLICATION.DTOs
{
	public class VacunacionesDto
    {
        public string Id { get; set; }
        public VacunacionCentroDto Centro { get; set; }
        public VacunacionVacunadorDto Vacunador { get; set; }
        public VacunacionVacunaDto Vacuna { get; set; }
        public VacunacionCaninoDto Canino { get; set; }
        public string Dosis { get; set; }
        public DateTime FechaProxima { get; set; }
    }

    public class VacunacionCentroDto
    {
        public string Id { get; set; }
		public string Nombre { get; set; }
		public string Direccion { get; set; }
	}
    public class VacunacionVacunadorDto
    {
        public string Id { get; set; }
		public string Nombre { get; set; }
	}
    public class VacunacionVacunaDto
    {
        public string Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public string Lote { get; set; }
	}
    public class VacunacionCaninoDto
    {
        public string Id { get; set; }
		public string Nombre { get; set; }
		public string Raza { get; set; }
		public string Color { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}
}
