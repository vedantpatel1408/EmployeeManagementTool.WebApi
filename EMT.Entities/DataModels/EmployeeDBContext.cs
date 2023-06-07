using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EMT.Entities.DataModels;

public partial class EmployeeDBContext : DbContext
{
    public EmployeeDBContext()
    {
    }

    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PasswordExpiry> PasswordExpiries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Attempts).HasColumnName("attempts");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(16)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasColumnName("gender");
            entity.Property(e => e.IsLocked).HasColumnName("is_locked");
            entity.Property(e => e.Lastname)
                .HasMaxLength(16)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalAttempts).HasColumnName("total_attempts");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).HasColumnName("Emp_id");
            entity.Property(e => e.Attempts)
                .HasDefaultValueSql("((0))")
                .HasColumnName("attempts");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(16)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(16)
                .HasColumnName("gender");
            entity.Property(e => e.IsLocked).HasColumnName("is_locked");
            entity.Property(e => e.Lastname)
                .HasMaxLength(16)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((0))")
                .HasColumnName("status");
            entity.Property(e => e.TotalAttempts).HasColumnName("total_attempts");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PasswordExpiry>(entity =>
        {
            entity.HasKey(e => e.ResetId);

            entity.ToTable("Password_Expiry");

            entity.Property(e => e.ResetId).HasColumnName("reset_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.PasswordUpdated)
                .HasColumnType("datetime")
                .HasColumnName("password_updated");
            entity.Property(e => e.PasswordexpiryDay).HasColumnName("passwordexpiry_day");
            entity.Property(e => e.PasswordexpiryStatus).HasColumnName("passwordexpiry_status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Emp).WithMany(p => p.PasswordExpiries)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Password_Expiry_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
