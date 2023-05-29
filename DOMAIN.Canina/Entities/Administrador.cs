using DOMAIN.Common;

namespace DOMAIN.Canina.Entities
{
	public class Administrador: AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
