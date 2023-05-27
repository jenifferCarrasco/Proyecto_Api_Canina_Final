using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
        public ICollection<Vacunaciones> Vacunaciones { get; set; }
    }
}
