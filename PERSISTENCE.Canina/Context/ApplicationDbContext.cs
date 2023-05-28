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
	public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {
        private readonly IDateTimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base (options)
        {

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<Caninos> Caninos { get; set; }
        public DbSet<Propietarios> Propietarios { get; set; }
        public DbSet<Vacunadores> Vacunadores { get; set; }
        public DbSet<Administradores> Administradores { get; set; }
        public DbSet<Moderadores> Moderadores { get; set; }
        public DbSet<Centros> Centros { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Vacunaciones> Vacunaciones { get; set; }
        public DbSet<Vacunas> Vacunas{ get; set; }
        public Assembly Assemble { get; private set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State) {

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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
