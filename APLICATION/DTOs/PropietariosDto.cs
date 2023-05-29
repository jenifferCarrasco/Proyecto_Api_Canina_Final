using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs
{
    public class PropietariosDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Canino> Caninos { get; set; }
    }
}
