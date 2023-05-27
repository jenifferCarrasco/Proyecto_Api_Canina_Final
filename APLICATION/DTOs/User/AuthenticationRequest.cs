using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.DTOs.User
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
