using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataService.DBEntity
{
    public partial class UseCaseDBDevContext : DbContext
    {
        public UseCaseDBDevContext()
        {
        }

        public UseCaseDBDevContext(DbContextOptions<UseCaseDBDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationCharacteristic> ApplicationCharacteristic { get; set; }
        public virtual DbSet<Ecf> Ecf { get; set; }
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<RoleActor> RoleActor { get; set; }
        public virtual DbSet<Tcf> Tcf { get; set; }
        public virtual DbSet<UseCase> UseCase { get; set; }
        public virtual DbSet<UseCaseActor> UseCaseActor { get; set; }
        public virtual DbSet<UseCaseEntity> UseCaseEntity { get; set; }
        public virtual DbSet<UseCaseStep> UseCaseStep { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=168.63.237.187;Database=UseCaseDBDev;Trusted_Connection=false;uid=sa;pwd=zaQ@1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Actor)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actor_RoleActor");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SourceCodeUrl).IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Technologies).IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicationCharacteristic>(entity =>
            {
                entity.HasIndex(e => e.ApplicationId)
                    .HasName("UQ_ApplicationId")
                    .IsUnique();

                entity.Property(e => e.ActualEfford).HasColumnName("Actual Efford");

                entity.HasOne(d => d.Application)
                    .WithOne(p => p.ApplicationCharacteristic)
                    .HasForeignKey<ApplicationCharacteristic>(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationCharacteristic_Application");
            });

            modelBuilder.Entity<Ecf>(entity =>
            {
                entity.ToTable("ECF");

                entity.HasIndex(e => e.ApplicationCharacteristicId)
                    .HasName("UQ_2_ProjectCharacteristicId")
                    .IsUnique();

                entity.Property(e => e.Ecfid).HasColumnName("ECFId");

                entity.HasOne(d => d.ApplicationCharacteristic)
                    .WithOne(p => p.Ecf)
                    .HasForeignKey<Ecf>(d => d.ApplicationCharacteristicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ECF_ApplicationCharacteristic");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Entity)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entity_Application");
            });

            modelBuilder.Entity<RoleActor>(entity =>
            {
                entity.Property(e => e.RoleActorId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tcf>(entity =>
            {
                entity.ToTable("TCF");

                entity.HasIndex(e => e.ApplicationCharacteristicId)
                    .HasName("UQ_ProjectCharacteristicId")
                    .IsUnique();

                entity.Property(e => e.Tcfid).HasColumnName("TCFId");

                entity.HasOne(d => d.ApplicationCharacteristic)
                    .WithOne(p => p.Tcf)
                    .HasForeignKey<Tcf>(d => d.ApplicationCharacteristicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TCF_ApplicationCharacteristic");
            });

            modelBuilder.Entity<UseCase>(entity =>
            {
                entity.Property(e => e.CreateBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.Stakeholder).IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UseCaseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.UseCase)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UseCase_Application");
            });

            modelBuilder.Entity<UseCaseActor>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.UseCaseId })
                    .HasName("PK_ActorUseCase");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.UseCaseActor)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActorUseCase_Actor");

                entity.HasOne(d => d.UseCase)
                    .WithMany(p => p.UseCaseActor)
                    .HasForeignKey(d => d.UseCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActorUseCase_UseCase");
            });

            modelBuilder.Entity<UseCaseEntity>(entity =>
            {
                entity.HasKey(e => new { e.UseCaseId, e.EntityId });

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.UseCaseEntity)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entity_UseCaseEntity");

                entity.HasOne(d => d.UseCase)
                    .WithMany(p => p.UseCaseEntity)
                    .HasForeignKey(d => d.UseCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UseCase_UseCaseEntity");
            });

            modelBuilder.Entity<UseCaseStep>(entity =>
            {
                entity.Property(e => e.Step)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.UseCase)
                    .WithMany(p => p.UseCaseStep)
                    .HasForeignKey(d => d.UseCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UseCase_UseCaseStep");
            });
        }
    }
}
