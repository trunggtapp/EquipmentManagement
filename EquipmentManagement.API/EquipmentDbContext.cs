using EquipmentManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.API
{
    public class EquipmentDbContext(DbContextOptions<EquipmentDbContext> options)
        : DbContext(options)
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Equipment> Equipment => Set<Equipment>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();
        public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();
        public DbSet<BorrowDetail> BorrowDetails => Set<BorrowDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(category => category.CategoryId);
                entity.Property(category => category.CategoryName)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(category => category.Description)
                    .HasMaxLength(255)
                    .IsUnicode();
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("Equipment", table => table.HasCheckConstraint(
                    "CK_Equipment_Status",
                    "[Status] IN ('Available', 'Borrowed', 'Maintenance')"));
                entity.HasKey(equipment => equipment.EquipmentId);
                entity.Property(equipment => equipment.EquipmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();
                entity.HasIndex(equipment => equipment.EquipmentCode)
                    .IsUnique();
                entity.Property(equipment => equipment.EquipmentName)
                    .HasMaxLength(150)
                    .IsUnicode()
                    .IsRequired();
                entity.Property(equipment => equipment.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(equipment => equipment.Note)
                    .HasMaxLength(255)
                    .IsUnicode();

                entity.HasOne(equipment => equipment.Category)
                    .WithMany(category => category.Equipments)
                    .HasForeignKey(equipment => equipment.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("Borrowers");
                entity.HasKey(borrower => borrower.BorrowerId);
                entity.Property(borrower => borrower.FullName)
                    .HasMaxLength(100)
                    .IsUnicode()
                    .IsRequired();
                entity.Property(borrower => borrower.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(borrower => borrower.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();
                entity.Property(borrower => borrower.Department)
                    .HasMaxLength(100)
                    .IsUnicode()
                    .IsRequired();
            });

            modelBuilder.Entity<BorrowRecord>(entity =>
            {
                entity.ToTable("BorrowRecords", table => table.HasCheckConstraint(
                    "CK_BorrowRecords_ReturnDate",
                    "[ExpectedReturnDate] >= [BorrowDate]"));
                entity.HasKey(record => record.BorrowRecordId);
                entity.Property(record => record.BorrowDate)
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(record => record.ExpectedReturnDate)
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(record => record.Notes)
                    .HasMaxLength(255)
                    .IsUnicode();

                entity.HasOne(record => record.Borrower)
                    .WithMany(borrower => borrower.BorrowRecords)
                    .HasForeignKey(record => record.BorrowerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BorrowDetail>(entity =>
            {
                entity.ToTable("BorrowDetails");
                entity.HasKey(detail => detail.BorrowDetailId);
                entity.Property(detail => detail.ActualReturnDate)
                    .HasColumnType("date");
                entity.Property(detail => detail.ReturnCondition)
                    .HasMaxLength(100)
                    .IsUnicode()
                    .IsRequired();

                entity.HasOne(detail => detail.BorrowRecord)
                    .WithMany(record => record.BorrowDetails)
                    .HasForeignKey(detail => detail.BorrowRecordId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(detail => detail.Equipment)
                    .WithMany(equipment => equipment.BorrowDetails)
                    .HasForeignKey(detail => detail.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(detail => detail.EquipmentId)
                    .HasDatabaseName("IX_BorrowDetails_EquipmentId_Active")
                    .HasFilter("[ActualReturnDate] IS NULL")
                    .IsUnique();
            });
        }
    }
}
