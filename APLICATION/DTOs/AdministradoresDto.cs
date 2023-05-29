using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs
{
    public class AdministradoresDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
