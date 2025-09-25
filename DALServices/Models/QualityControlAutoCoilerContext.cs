using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Services.Models
{
    public partial class QualityControlAutoCoilerContext : IdentityDbContext<QualityControlAutoCoilerUser, IdentityRole<long>, long>
    {
        public QualityControlAutoCoilerContext()
        {
        }

        public QualityControlAutoCoilerContext(DbContextOptions<QualityControlAutoCoilerContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<ProductionLog> ProductionLogs { get; set; }
        public virtual DbSet<SizeCategory> SizeCategories { get; set; }
        
		public virtual DbSet<PermissionTemplate> PermissionTemplates { get; set; }
        public virtual DbSet<PermissionTemplateDetail> PermissionTemplateDetails { get; set; }
        public virtual DbSet<ApplicationFunctionality> ApplicationFunctionalities { get; set; }
        public virtual DbSet<ApplicationModule> ApplicationModules { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

			modelBuilder.Entity<PermissionTemplate>(entity =>
            {
                entity.ToTable("PermissionTemplate");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.TemplateName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PermissionTemplateDetail>(entity =>
            {
                entity.ToTable("PermissionTemplateDetail");

                entity.HasOne(d => d.Template).WithMany(p => p.PermissionTemplateDetails)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionTemplateDetail_PermissionTemplate");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ColorNameInUrdu)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Machines");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.NameInUrdu)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SizeCategory>(entity =>
            {
                entity.ToTable("SizeCategory");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductionLog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Producti__3214EC07E3C02761");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
                entity.Property(e => e.Bp)
                    .HasMaxLength(50)
                    .HasColumnName("BP");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.DrumNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Np)
                    .HasMaxLength(50)
                    .HasColumnName("NP");
                entity.Property(e => e.Reason).HasMaxLength(255);
                entity.Property(e => e.Remarks).HasMaxLength(255);

                //entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.ProductionLogApprovedByNavigations)
                //    .HasForeignKey(d => d.ApprovedBy)
                //    .HasConstraintName("FK_ProductionLogs_Approved_AspNetUsers");

                //entity.HasOne(d => d.Color).WithMany(p => p.ProductionLogs)
                //    .HasForeignKey(d => d.ColorId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_ProductionLogs_Colors");

                //entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionLogCreatedByNavigations)
                //    .HasForeignKey(d => d.CreatedBy)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_ProductionLogs_Created_AspNetUsers");

                //entity.HasOne(d => d.Size).WithMany(p => p.ProductionLogs)
                //    .HasForeignKey(d => d.SizeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_ProductionLogs_SizeCategory");
            });

            modelBuilder.Entity<ApplicationFunctionality>(entity =>
            {
                entity.Property(e => e.ActionMethodName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.FunctionalityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ApplicationModule).WithMany(p => p.ApplicationFunctionalities)
                    .HasForeignKey(d => d.ApplicationModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationFunctionalities_ApplicationModules");
            });

            modelBuilder.Entity<ApplicationModule>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_FormDetail");

                entity.Property(e => e.ControllerName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.IconCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAccess>(entity =>
            {
                entity.ToTable("UserAccess");

                entity.Property(e => e.FormName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
