using DOMAIN.Canina;

namespace APLICATION.DTOs
{
	public class VacunasDto
    {
		public string Id { get; set; }
		public string Nombre { get; set; }
		public string Laboratorio { get; set; }
		public string Descripcion { get; set; }
		public int CantidadDisponible { get; set; }
		public Estados Estado { get; set; }
	}
}
