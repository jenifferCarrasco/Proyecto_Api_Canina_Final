using DOMAIN.Canina.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERSISTENCE.Canina.Configuration
{
	public class InventarioConfig : IEntityTypeConfiguration<Inventario>
	{
		public void Configure(EntityTypeBuilder<Inventario> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.VacunaId)
				.IsRequired();

			builder.Property(p => p.CreatedBy)
				.HasMaxLength(30);
			builder.Property(p => p.LastModifiedBy)
				.HasMaxLength(30);

		}
	}
}
