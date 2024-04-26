using Business.Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Layer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            /* Each DbContext instance tracks changes made to entities. These tracked entities in turn drive the changes to the database when SaveChanges is called.
             * The tracker will not work if the changes are not applied directly to the model,
             * as in this project that uses DTO (Data transfer object), 
             * in addition there is the problem of concurrency, since the ORM has an object under observation 
             * and a new one with the same id is trying to be inserted or updated.
             */
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * All classes that extend IEntityTypeConfiguration<> and that were Set in Assembly:AppDbContext will be used for database modeling. 
             * This is what the line of code below does, that is, the mapping settings will be applied.
             */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            /*
             * For all mappings that have ForeignKeys, override the cascade delete behavior to set null. 
             * This is what the line of code below does:
             */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistrationDate").IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

