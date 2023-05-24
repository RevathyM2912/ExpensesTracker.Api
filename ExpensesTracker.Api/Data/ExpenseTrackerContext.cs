using System;
using System.Collections.Generic;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data;

public partial class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExpenseDetail> ExpenseDetails { get; set; }

    public virtual DbSet<IncomeDetail> IncomeDetails { get; set; }

    public virtual DbSet<LoginTb> LoginTbs { get; set; }

    public virtual DbSet<ReminderDetail> ReminderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ExpenseDetail>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PRIMARY");

            entity.ToTable("expense_details");

            entity.HasIndex(e => e.ProEmail, "Pro_Email_idx");

            entity.Property(e => e.ExpenseId).HasColumnName("Expense_id");
            entity.Property(e => e.ExpenseAmount)
                .HasPrecision(10)
                .HasColumnName("Expense_Amount");
            entity.Property(e => e.ExpenseCategory)
                .HasMaxLength(45)
                .HasColumnName("Expense_Category");
            entity.Property(e => e.ExpenseDate).HasColumnName("Expense_Date");
            entity.Property(e => e.ExpenseDescription)
                .HasMaxLength(45)
                .HasDefaultValueSql("'No Description'")
                .HasColumnName("Expense_Description");
            entity.Property(e => e.ProEmail)
                .HasMaxLength(45)
                .HasColumnName("Pro_Email");

            entity.HasOne(d => d.ProEmailNavigation).WithMany(p => p.ExpenseDetails)
                .HasForeignKey(d => d.ProEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pro_Email");
        });

        modelBuilder.Entity<IncomeDetail>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PRIMARY");

            entity.ToTable("income_details");

            entity.HasIndex(e => e.Email, "Email_idx");

            entity.Property(e => e.IncomeId).HasColumnName("Income_id");
            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.Category).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(45);
        });

        modelBuilder.Entity<LoginTb>(entity =>
        {
            entity.HasKey(e => e.ProEmail).HasName("PRIMARY");

            entity.ToTable("login_tb");

            entity.Property(e => e.ProEmail)
                .HasMaxLength(45)
                .HasColumnName("Pro_Email");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("Pro_Name");
            entity.Property(e => e.ProPassword)
                .HasMaxLength(45)
                .HasColumnName("Pro_Password");
        });

        modelBuilder.Entity<ReminderDetail>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PRIMARY");

            entity.ToTable("reminder_details");

            entity.HasIndex(e => e.Email, "Email_idx");

            entity.Property(e => e.ReminderId).HasColumnName("Reminder_id");
            entity.Property(e => e.DueDate).HasColumnName("Due_date");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.RemAmount)
                .HasPrecision(10)
                .HasColumnName("Rem_amount");
            entity.Property(e => e.RemDescription)
                .HasMaxLength(45)
                .HasColumnName("Rem_description");
            entity.Property(e => e.RemType)
                .HasMaxLength(45)
                .HasColumnName("Rem_type");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.ReminderDetails)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Email");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
