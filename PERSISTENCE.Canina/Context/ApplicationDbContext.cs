using Application.Interface;
using DOMAIN.Canina.Entities;
using DOMAIN.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Context
{
	public class ApplicationDbContext : IdentityDbContext<Usuario>
	{
		private readonly IDateTimeService _dateTime;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base(options)
		{

			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			_dateTime = dateTime;
		}

		public DbSet<Canino> Caninos { get; set; }
		public DbSet<Propietario> Propietarios { get; set; }
		public DbSet<Vacunador> Vacunadores { get; set; }
		public DbSet<Administrador> Administradores { get; set; }
		public DbSet<Centro> Centros { get; set; }
		public DbSet<Cita> Citas { get; set; }
		public DbSet<Vacunacion> Vacunaciones { get; set; }
		public DbSet<Vacuna> Vacunas { get; set; }
		public DbSet<Inventario> Inventario { get; set; }
		public Assembly Assemble { get; private set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{

			foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
			{
				switch (entry.State)
				{

					case EntityState.Added:
						entry.Entity.Created = _dateTime.NowUTC;
						break;
					case EntityState.Modified:
						entry.Entity.LastModified = _dateTime.NowUTC;
						break;

				}
			}
			return base.SaveChangesAsync(cancellationToken);

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}


}
