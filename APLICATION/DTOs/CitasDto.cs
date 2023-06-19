using DOMAIN.Canina;
using System;

namespace APLICATION.DTOs
{
	public class CitasDto
    {
		public Guid Id { get; set; }
		public DateTime FechaCita { get; set; }
		public Estados Estatus { get; set; }
		public CitasCentroDto Centro { get; set; }
		public CitasPropietarioDto Propietario { get; set; }
		public CitasCaninoDto Canino { get; set; }
		public CitasVacunadorDto Vacunador { get; set; }
	}

	public class CitasPropietarioDto
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string NombreCompleto => $"{Nombre} {Apellido}";
		public string Cedula { get; set; }
	}
	public class CitasCaninoDto
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
		public Guid PropietarioId { get; set; }
	}

	public class CitasCentroDto
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
	}

	public class CitasVacunadorDto
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string NombreCompleto => $"{Nombre} {Apellido}";
	}

}
