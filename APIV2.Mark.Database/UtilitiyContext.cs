using APIV2.Mark.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APIV2.Mark.Database
{
    public class UtilitiyContext : DbContext
    {
        public UtilitiyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ChartOfAccount> ChartOfAccount { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalDetail> JournalDetails { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseInventory> WarehouseInventories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItems> PurchaseItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Bank> Banks { get; set; } 
        public DbSet<Treasury> Treasury { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ChartOfAccount>()
            .HasOne(s => s.AccountParent)
             .WithMany()
             .HasForeignKey(e => e.ParentId);
        }
    }
}