using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Canina.Configuration
{
    public class PropietarioConfig : IEntityTypeConfiguration<DOMAIN.Canina.Entities.Propietario>
    {
        public void Configure(EntityTypeBuilder<DOMAIN.Canina.Entities.Propietario> builder)
        {
            builder.ToTable("Propietarios");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Apellido)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Cedula)
                .HasMaxLength(13)
                .IsRequired();
            builder.Property(p => p.Direccion)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Telefono)
                .HasMaxLength(12)
                .IsRequired();
            builder.Property(p => p.UsuarioId);
            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
