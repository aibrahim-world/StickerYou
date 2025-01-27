using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Customers.Infrastructure
{
    public class CustomerDbContext: DbContext
    {
        private const string _schemaName = "customers";
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
    : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerEventLog> CustomerEventLogs { get; set; }
        public DbSet<Address> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustomerEventLogs)
                .WithOne()
                .HasForeignKey(cel => cel.CustomerId);


            modelBuilder.Entity<Customer>()
                .Property(a => a.Name)
                .HasMaxLength(300);

            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLine1)
                .HasMaxLength(300);


            modelBuilder.HasDefaultSchema(_schemaName);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<CustomerEventLog>().ToTable("CustomerEventLog");
            modelBuilder.Entity<Address>().ToTable("Address");


        }
    }
}
