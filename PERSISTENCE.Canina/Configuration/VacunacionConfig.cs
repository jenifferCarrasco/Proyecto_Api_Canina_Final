using System;
using System.Collections.Generic;
using System.Text;
using DOMAIN.Canina.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Canina.Configuration
{
    public class VacunacionConfig : IEntityTypeConfiguration<Vacunaciones>
    {
        public void Configure(EntityTypeBuilder<Vacunaciones> builder)
        {
            builder.ToTable("Vacunaciones");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CentroId)
                .IsRequired();
            builder.Property(p => p.CaninoId)
                .IsRequired();
            builder.Property(p => p.VacunadorId)
                .IsRequired();
            builder.Property(p => p.VacunaId)
                .IsRequired();
            builder.Property(p => p.Dosis)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(p => p.FechaProxima)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);

        }
    }
}
