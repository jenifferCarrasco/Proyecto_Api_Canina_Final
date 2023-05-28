using DOMAIN.Common;

namespace DOMAIN.Canina.Entities
{
	public class Administradores: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
