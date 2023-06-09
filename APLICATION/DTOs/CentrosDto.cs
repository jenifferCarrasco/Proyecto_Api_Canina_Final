﻿using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs
{
    public class CentrosDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Estados Estatus { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<Vacunacion> Vacunaciones { get; set; }
    }
}
