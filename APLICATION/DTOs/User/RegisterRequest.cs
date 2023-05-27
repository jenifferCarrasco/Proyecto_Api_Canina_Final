using APLICATION.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs.User
{
    public class RegisterRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        //public string Cedula { get; set; }
        //public string Direccion { get; set; }
        public Roles  Roles { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
