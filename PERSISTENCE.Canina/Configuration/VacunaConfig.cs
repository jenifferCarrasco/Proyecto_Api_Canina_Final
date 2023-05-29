using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DOMAIN.Canina.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Canina.Configuration
{
    public class VacunaConfig : IEntityTypeConfiguration<Vacuna>
    {
        public void Configure(EntityTypeBuilder<Vacuna> builder)
        {
            builder.ToTable("Vacunas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Laboratorio)
                .HasMaxLength(80)
                .IsRequired();
            //builder.Property(p => p.FechaCaducidad)
            //    .IsRequired();
            builder.Property(p => p.Descripcion)
                .HasMaxLength(350)
                .IsRequired();
            //builder.Property(p => p.Lote)
            //    .HasMaxLength(20)
            //    .IsRequired();
            //builder.Property(p => p.Estatus);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
