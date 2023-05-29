using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERSISTENCE.Canina.Configuration
{
    public class CentroConfig : IEntityTypeConfiguration<DOMAIN.Canina.Entities.Centro>
    {
        public void Configure(EntityTypeBuilder<DOMAIN.Canina.Entities.Centro> builder)
        {
            builder.ToTable("Centros");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nombre)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Direccion)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(p => p.Estatus);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
