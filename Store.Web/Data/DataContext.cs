using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Common.Entities;
using Store.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CotizacionEntity> CotizacionEntities { get; set; }
        public DbSet<CategoriaEntity> CategoriaEntities { get; set; }
        public DbSet<CotizacionDetalleEntity> CotizacionDetalleEntities { get; set; }

        //public DbSet<CompraDetalleEntity> CompraDetalleEntities { get; set; }
        //public DbSet<CompraEntity> CompraEntities { get; set; }
        public DbSet<ProductoEntity> ProductoEntities { get; set; }

        public DbSet<ProveedorEntity> ProveedorEntities { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // string userId = "Desarrollo";ProductoEntities

            System.Collections.Generic.IEnumerable<EntityEntry<IAudit>> modifiedEntries = ChangeTracker.Entries<IAudit>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<IAudit> entry in modifiedEntries)
            {
                //   entry.Entity.UserM = userId;
                entry.Entity.FechaM = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    //    entry.Entity.UserC = userId;
                    entry.Entity.FechaC = DateTime.Now;
                }
            }

            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory<AuditEntity>(o => { });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProveedorEntity>()
                .HasIndex(t => t.Nombre)
                .IsUnique();

            modelBuilder.Entity<ProveedorEntity>()
                .HasIndex(t => t.NRC)
                .IsUnique();

            modelBuilder.Entity<ProveedorEntity>()
                .HasIndex(t => t.NIT)
                .IsUnique();

            modelBuilder.Entity<ProductoEntity>()
                .HasIndex(t => t.Nombre)
                .IsUnique();
        }
    }
}