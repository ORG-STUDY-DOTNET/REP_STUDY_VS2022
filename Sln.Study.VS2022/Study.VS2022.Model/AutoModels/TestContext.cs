using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Study.VS2022.Model.AutoModels
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Torder> Torders { get; set; } = null!;
        public virtual DbSet<Tuser> Tusers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=root;port=3306;database=studyvs2022", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Torder>(entity =>
            {
                entity.HasKey(e => e.ToGuid)
                    .HasName("PRIMARY");

                entity.ToTable("TOrder");

                entity.Property(e => e.ToGuid).HasColumnName("TO_GUID");

                entity.Property(e => e.ToPrice)
                    .HasColumnType("int(11)")
                    .HasColumnName("TO_Price");
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.HasKey(e => e.TuGuid)
                    .HasName("PRIMARY");

                entity.ToTable("TUser");

                entity.Property(e => e.TuGuid).HasColumnName("TU_GUID");

                entity.Property(e => e.TuAccount)
                    .HasMaxLength(20)
                    .HasColumnName("TU_Account");

                entity.Property(e => e.TuPassword)
                    .HasMaxLength(255)
                    .HasColumnName("TU_Password");

                entity.Property(e => e.TuRealName)
                    .HasMaxLength(255)
                    .HasColumnName("TU_RealName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
