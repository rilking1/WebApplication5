using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Data;

public partial class DbcoursesContext : DbContext
{
    public DbcoursesContext()
    {
    }

    public DbcoursesContext(DbContextOptions<DbcoursesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<DifficultyLevel> DifficultyLevels { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeachersSpecialization> TeachersSpecializations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9DBVOBO\\SQLEXPRESS; Database=DBCourses; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.PhotoUrlId)
                .HasConstraintName("FK_AspNetUsers_Photo");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category1)
                .HasMaxLength(255)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.Id, "UQ_Courses_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DifficultyLevelId).HasColumnName("DifficultyLevelID");
            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
            entity.Property(e => e.TeachersId)
                .HasMaxLength(450)
                .HasColumnName("TeachersID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Courses_Categorys");

            entity.HasOne(d => d.DifficultyLevel).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DifficultyLevelId)
                .HasConstraintName("FK_Courses_DifficultyLevels");

            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Courses)
                .HasForeignKey(d => d.PhotoUrlId)
                .HasConstraintName("FK_Courses_Photo");

            entity.HasOne(d => d.Teachers).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeachersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_AspNetUsers");
        });

        modelBuilder.Entity<DifficultyLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DifficultyLevel");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DifLevel).HasMaxLength(255);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.ToTable("Photo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PhotoUrl).HasMaxLength(255);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rating");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Rating1).HasColumnName("Rating");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Students");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Specialization");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Specialization1)
                .HasMaxLength(255)
                .HasColumnName("Specialization");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Students_Courses");

            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Students)
                .HasForeignKey(d => d.PhotoUrlId)
                .HasConstraintName("FK_Students_Photo");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.PhotoUrlId)
                .HasConstraintName("FK_Teachers_Photo");
        });

        modelBuilder.Entity<TeachersSpecialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TeachersSpecializationsID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.TeachersId).HasColumnName("TeachersID");

            entity.HasOne(d => d.Specialization).WithMany(p => p.TeachersSpecializations)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeachersSpecializationsID_Specializations");

            entity.HasOne(d => d.Teachers).WithMany(p => p.TeachersSpecializations)
                .HasForeignKey(d => d.TeachersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeachersSpecializationsID_Teachers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
