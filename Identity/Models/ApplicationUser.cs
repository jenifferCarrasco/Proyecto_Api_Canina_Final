using APLICATION.Enum;
using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser { 
        public string Nombre{ get; set; }
        public string Apellido { get; set; }
      
        public Roles Roles { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }

        //public ICollection<Citas> Citas { get; set; }
        public ICollection<Caninos> Caninos { get; set; }
        //public ICollection<Vacunaciones> Vacunaciones { get; set; }
    }
}
