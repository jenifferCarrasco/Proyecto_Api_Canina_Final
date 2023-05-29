using Microsoft.AspNetCore.Identity;

namespace DOMAIN.Canina.Entities
{
	public class Usuario:IdentityUser
    {
        public string TipoUsuario { get; set; }
        public Propietario Propietario { get; set; }
        public Administrador Administrador { get; set; }

    }
    
}
