using System;

namespace APLICATION.DTOs
{
	public class CaninoDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Estatus { get; set; }

        //public string PropietarioId { get; set; }
        //public Propietario Propietario { get; set; }
        //public virtual ICollection<Vacunacion> Vacunaciones { get; set; }
        //public virtual ICollection<Cita> Citas { get; set; }
    }
}
