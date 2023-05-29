using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using APLICATION.DTOs.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace APLICATION.DTOs
{
    public class CaninoDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool Sexo { get; set; }
        public string Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string PropietarioId { get; set; }
        public Propietario Propietario { get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Vacunacion> Vacunaciones { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }



    }
}
