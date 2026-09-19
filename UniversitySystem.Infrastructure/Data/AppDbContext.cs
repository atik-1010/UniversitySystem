using Microsoft.EntityFrameworkCore;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentMark> StudentMarks { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Alumni> Alumni { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.StudentCode).IsRequired();
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Email).IsRequired();

                entity.HasOne(x => x.Department)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Code).IsRequired();
                entity.Property(x => x.Credit).IsRequired();

                entity.HasOne(x => x.Department)
                    .WithMany(x => x.Courses)
                    .HasForeignKey(x => x.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Teacher
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();

                entity.HasOne(x => x.Department)
                    .WithMany(x => x.Teachers)
                    .HasForeignKey(x => x.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // StudentMark
            modelBuilder.Entity<StudentMark>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Score).IsRequired();
                entity.Property(x => x.GradePoint).IsRequired();
                entity.Property(x => x.Grade).IsRequired();

                entity.HasOne(x => x.Student)
                    .WithMany(x => x.StudentMarks)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Course)
                    .WithMany()
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Username).IsUnique();
            });

            // Notice
            modelBuilder.Entity<Notice>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Description).IsRequired();
                entity.Property(x => x.Date).IsRequired();
            });

            // Alumni
            modelBuilder.Entity<Alumni>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.GraduationYear).IsRequired();

                entity.HasOne(x => x.Department)
                    .WithMany()
                    .HasForeignKey(x => x.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Marks).IsRequired();

                entity.HasOne(x => x.Student)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Course)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.EnrolledOn)
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
