using DOMAIN.Canina.Enum;

namespace APLICATION.DTOs
{
	public class VacunadoresDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
		public Generos Sexo { get; set; }
	}
}
