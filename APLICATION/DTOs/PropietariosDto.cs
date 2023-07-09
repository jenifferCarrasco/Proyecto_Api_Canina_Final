using DOMAIN.Canina.Enum;
using System;

namespace APLICATION.DTOs
{
	public class PropietariosDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Cedula { get; set; }
		public Generos Sexo { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
