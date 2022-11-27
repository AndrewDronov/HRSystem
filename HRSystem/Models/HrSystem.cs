using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HRSystem.Models
{
    public partial class HrSystem : DbContext
    {
        public HrSystem()
        {
        }

        public HrSystem(DbContextOptions<HrSystem> options)
            : base(options)
        {
        }

        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<TransferHistory> TransferHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("division");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
                
                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(100)
                    .HasColumnName("middle_name");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("employee_FK");
            });
            
            modelBuilder.Entity<TransferHistory>(entity =>
            {
                entity.ToTable("transfer_history");
                
                entity.Property(e => e.TransferId).HasColumnName("transfer_id");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("employee_id");

                entity.Property(e => e.DivisionId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("division_id");
                
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("created_at");
                
                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("transfer_history_FK_1");
                
                
                entity.HasOne(d => d.Division)
                    .WithMany()
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("transfer_history_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
