using DOMAIN.Canina.Enum;

namespace APLICATION.DTOs.User
{
	public class RegisterPropietarioRequest
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Cedula { get; set; }
		public Generos Sexo { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
