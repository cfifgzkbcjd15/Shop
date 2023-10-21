using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopBase.Data
{
    public partial class bugZillaContext : DbContext
    {
        public bugZillaContext()
        {
        }

        public bugZillaContext(DbContextOptions<bugZillaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<UnicalLog> UnicalLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bugZilla;Username=postgres;Password=ihesop69");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("logs");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogId)
                    .HasColumnType("character varying")
                    .HasColumnName("log_id");

                entity.Property(e => e.Text)
                    .HasColumnType("character varying")
                    .HasColumnName("text");
            });

            modelBuilder.Entity<UnicalLog>(entity =>
            {
                entity.ToTable("unicalLogs");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.LogId)
                    .HasColumnType("character varying")
                    .HasColumnName("log_id");

                entity.Property(e => e.Text)
                    .HasColumnType("character varying")
                    .HasColumnName("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
