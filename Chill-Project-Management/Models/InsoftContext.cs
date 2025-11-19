using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Chill_Project_Management.Models;

public partial class InsoftContext : DbContext
{
    public InsoftContext()
    {
    }

    public InsoftContext(DbContextOptions<InsoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KanbanTask> KanbanTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Insoft;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KanbanTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kanban_t__3213E83F1BD613CD");

            entity.ToTable("kanban_tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasDefaultValue("medium")
                .HasColumnName("priority");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasDefaultValue("todo")
                .HasColumnName("state");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .HasColumnName("title");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.KanbanTasks)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_kanban_tasks_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F3E29EA9E");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616424F0C896").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
