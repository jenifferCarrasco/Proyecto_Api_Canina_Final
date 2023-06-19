using DOMAIN.Canina.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Canina.Configuration
{
	public class CitaConfig : IEntityTypeConfiguration<Cita>
	{
		public void Configure(EntityTypeBuilder<Cita> builder)
		{
			builder.ToTable("Citas");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.FechaCita)
				.IsRequired();
			builder.Property(p => p.CentroId)
				.IsRequired();
			builder.Property(p => p.VacunadorId)
				.IsRequired();
			builder.Property(p => p.PropietarioId)
				.IsRequired();
			builder.Property(p => p.CaninoId)
				.IsRequired();
			builder.Property(p => p.Estatus);

			builder.Property(p => p.CreatedBy)
				.HasMaxLength(30);
			builder.Property(p => p.LastModifiedBy)
				.HasMaxLength(30);
			
			builder
				.HasOne(c => c.Propietario)
				.WithMany(p => p.Citas)
				.HasForeignKey(c => c.PropietarioId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Centro)
				.WithMany(p => p.Citas)
				.HasForeignKey(c => c.CentroId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Canino)
				.WithMany(p => p.Citas)
				.HasForeignKey(c => c.CaninoId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Vacunador)
				.WithMany(p => p.Citas)
				.HasForeignKey(c => c.VacunadorId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
