using Microsoft.EntityFrameworkCore;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // =========================
    // TABLES
    // =========================

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<AppUser> Users => Set<AppUser>();

    // STEP 6–8 MODULES
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<StudentMark> StudentMarks => Set<StudentMark>();

    // =========================
    // MODEL CONFIG
    // =========================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // STUDENT → DEPARTMENT
        // =========================

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.DepartmentId)
                .IsRequired();

            entity
                .HasOne(x => x.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // COURSE → DEPARTMENT
        // =========================

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Credit)
                .IsRequired();

            entity
                .HasOne(x => x.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // TEACHER → DEPARTMENT
        // =========================

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity
                .HasOne(x => x.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // STUDENT MARKS
        // =========================

        modelBuilder.Entity<StudentMark>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Marks)
                .IsRequired();

            entity
                .HasOne(x => x.Student)
                .WithMany(s => s.StudentMarks)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // USER CONFIG
        // =========================

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity
                .HasIndex(x => x.Username)
                .IsUnique();

            entity.Property(x => x.Username)
                .HasMaxLength(100);

            entity.Property(x => x.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Student");

            entity.Property(x => x.PasswordHash)
                .HasColumnType("nvarchar(max)");
        });
    }
}