﻿using APLICATION.Enum;

namespace APLICATION.DTOs.User
{
	public class RegisterRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Roles?  Rol { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
