using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace V01_SignIn.Data.Models
{
    public partial class TESTContext : DbContext
    {
        public TESTContext()
        {
        }

        public TESTContext(DbContextOptions<TESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=DESKTOP-ONNKC5V\SQLEXPRESS;Database=TEST;User ID=sa;Password=Local123;MultipleActiveResultSets=true");
                optionsBuilder.UseSqlServer("Server=DESKTOP-ONNKC5V\\SQLEXPRESS;Database=TEST;Trusted_Connection=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Croatian_CI_AS");

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.LanguageId).HasMaxLength(5);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.StateId).HasMaxLength(1);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LanguageId).HasMaxLength(5);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Username).HasMaxLength(30);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.LanguageId);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.StateId);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserRoleId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
