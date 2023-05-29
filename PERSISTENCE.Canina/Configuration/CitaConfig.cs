using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERSISTENCE.Canina.Configuration
{
    public class CitaConfig : IEntityTypeConfiguration<DOMAIN.Canina.Entities.Cita>
    {
        public void Configure(EntityTypeBuilder<DOMAIN.Canina.Entities.Cita> builder)
        {
            builder.ToTable("Citas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FechaCita)
                .IsRequired();
            builder.Property(p => p.CentroId)
                .IsRequired();
            builder.Property(p => p.VacunadorId)
                .IsRequired();
            //builder.Property(p => p.PropietarioID)
            //    .IsRequired();
            builder.Property(p => p.CaninoId)
                .IsRequired();
            builder.Property(p => p.Estatus);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
