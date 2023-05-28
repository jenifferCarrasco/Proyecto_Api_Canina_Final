using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERSISTENCE.Canina.Configuration
{
    public class CaninoConfig : IEntityTypeConfiguration<DOMAIN.Canina.Entities.Caninos>
    {
        public void Configure(EntityTypeBuilder<DOMAIN.Canina.Entities.Caninos> builder)
        {
            builder.ToTable("Caninos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Raza)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.FechaNacimiento)
                .IsRequired();
            builder.Property(p => p.Color)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(p => p.Peso)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(p => p.Estatus);
            builder.Property(p => p.PropietarioId);
            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
