using Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base (options)
        {

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }
        public DbSet<Cliente> Clientes { get; set; }
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
        }
    }

    
}
