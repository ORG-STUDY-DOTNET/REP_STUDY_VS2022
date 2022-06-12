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

        public virtual DbSet<TOrder> TOrders { get; set; } = null!;
        public virtual DbSet<TUser> TUsers { get; set; } = null!;

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

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.TO_GUID)
                    .HasName("PRIMARY");

                entity.ToTable("TOrder");

                entity.Property(e => e.TO_Price).HasColumnType("int(11)");
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.TU_GUID)
                    .HasName("PRIMARY");

                entity.ToTable("TUser");

                entity.Property(e => e.TU_Account).HasMaxLength(20);

                entity.Property(e => e.TU_Password).HasMaxLength(255);

                entity.Property(e => e.TU_RealName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
