using FinLog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinLog.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Budget> Budgets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull); // Khi xoá Danh mục -> Giao dịch k bị xoá, chỉ set CategoryId = null

                entity.HasOne(e => e.Account)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(e => e.AccountId)
                      .OnDelete(DeleteBehavior.SetNull); // Khi xoá Tài khoản -> Giao dịch k bị xoá, chỉ set AccountId = null
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
            
            // Seed dữ liệu mặc định
            // modelBuilder.Entity<Category>().HasData(...)
        }
    }
}
