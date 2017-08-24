using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RfidSPA.Models.Entities;

namespace RfidSPA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Anagrafica> Anagrafica { get; set; }
        public DbSet<RfidDevice> RfidDevice { get; set; }
        public DbSet<RfidDeviceHistory> RfidDeviceHistory { get; set; }
        public DbSet<RfidDeviceTransaction> RfidDeviceTransaction { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreUsers> StoreUsers { get; set; }

        public DbSet<TypeDeviceHistoryOperation> TypeDeviceHistoryOperations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<RfidDevice>().ToTable("RfidDevice");
            builder.Entity<Anagrafica>().ToTable("Anagrafica");
            builder.Entity<RfidDeviceHistory>().ToTable("RfidDeviceHistory");
            builder.Entity<RfidDeviceTransaction>().ToTable("RfidDeviceTransaction");
        }
        
    }
}