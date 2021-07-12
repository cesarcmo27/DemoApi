
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<Site> WebSites { get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<ItemVendor> ItemVendor { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<PharmacyInventory> PharmacyInventory { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>()
                .HasOne(u => u.Order)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(u => u.OrderId);

            builder.Entity<Pharmacy>()
            .HasOne(u => u.Hospital)
            .WithMany(a => a.Pharmacies)
            .HasForeignKey(o => o.HospitalId);

            builder.Entity<Item>()
            .HasOne(u => u.ItemVendor)
            .WithMany(a => a.Items)
            .HasForeignKey(o => o.ItemVendorId);

            builder.Entity<PharmacyInventory>(b => {
                b.HasKey(k => new {k.PharmacyId,k.ItemId});

                b.HasOne(p => p.Pharmacy)
                .WithMany(f => f.PharmaciesInventory)
                .HasForeignKey(u => u.PharmacyId);

                b.HasOne(p => p.Item)
                  .WithMany(f => f.Pharmacies)
                .HasForeignKey(u => u.PharmacyId);
            });
            
        }

    }
}